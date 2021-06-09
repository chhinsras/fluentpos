using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Modules.Identity.Core.Settings;
using FluentPOS.Shared.Abstractions.Interfaces.Services.Identity;
using FluentPOS.Shared.Abstractions.Wrapper;
using FluentPOS.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Identity.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly RoleManager<ExtendedIdentityRole> _roleManager;
        private readonly JwtSettings _config;

        public TokenService(
            UserManager<ExtendedIdentityUser> userManager, RoleManager<ExtendedIdentityRole> roleManager,
            IOptions<JwtSettings> config, SignInManager<ExtendedIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config.Value;
        }

        public async Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user == null)
            {
                return Result<TokenResponse>.Fail("User Not Found.");
            }
            if (!user.IsActive)
            {
                return Result<TokenResponse>.Fail("User Not Active. Please contact the administrator.");
            }
            if (!user.EmailConfirmed)
            {
                return Result<TokenResponse>.Fail("E-Mail not confirmed.");
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.password);
            if (!passwordValid)
            {
                return Result<TokenResponse>.Fail("Invalid Credentials.");
            }

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_config.refreshTokenExpirationInDays);
            await _userManager.UpdateAsync(user);

            var token = await GenerateJwtAsync(user, ipAddress);
            var response = new TokenResponse(token, user.RefreshToken,user.RefreshTokenExpiryTime);
            return Result<TokenResponse>.Success(response);
        }

        public async Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
        {
            if (request is null)
            {
                return Result<TokenResponse>.Fail("Invalid Client Token.");
            }
            var userPrincipal = GetPrincipalFromExpiredToken(request.token);
            var userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return Result<TokenResponse>.Fail("User Not Found.");
            if (user.RefreshToken != request.refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return Result<TokenResponse>.Fail("Invalid Client Token.");
            var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user, ipAddress));
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_config.refreshTokenExpirationInDays);
            await _userManager.UpdateAsync(user);
            var response = new TokenResponse (token,user.RefreshToken,user.RefreshTokenExpiryTime);
            return Result<TokenResponse>.Success(response);
        }

        private async Task<string> GenerateJwtAsync(ExtendedIdentityUser user, string ipAddress)
        {
            var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user, ipAddress));
            return token;
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(ExtendedIdentityUser user, string ipAddress)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
                var thisRole = await _roleManager.FindByNameAsync(roles[i]);
                var allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole);
                foreach (var permission in allPermissionsForThisRoles)
                {
                    permissionClaims.Add(permission);
                }
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
                new Claim("ipAddress", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);
            return claims;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(_config.tokenExpirationInMinutes),
               signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);
            return encryptedToken;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token.");
            }

            return principal;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = Encoding.UTF8.GetBytes(_config.key);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }
    }
}
