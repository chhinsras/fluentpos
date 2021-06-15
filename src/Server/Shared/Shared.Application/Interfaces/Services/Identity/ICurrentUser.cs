using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace FluentPOS.Shared.Application.Interfaces.Services.Identity
{
    public interface ICurrentUser
    {
        string Name { get; }

        Guid GetUserId();

        string GetUserEmail();

        bool IsAutenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetUserClaims();

        HttpContext GetHttpContext();
    }
}