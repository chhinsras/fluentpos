namespace FluentPOS.Modules.Identity.Core.Settings
{
    public class JwtSettings
    {
        public string key { get; set; }
        public int tokenExpirationInMinutes { get; set; }
        public int refreshTokenExpirationInDays { get; set; }
    }
}