using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Identity.Roles
{
    public class PermissionRequest
    {
        public string RoleId { get; set; }
        public IList<RoleClaimRequest> RoleClaims { get; set; }
    }
}