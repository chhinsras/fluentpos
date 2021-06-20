using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Identity.Core.Abstractions
{
    public interface IRoleService
    {
        Task<Result<List<RoleResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleResponse>> GetByIdAsync(string id);

        Task<Result<string>> SaveAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(string id);

        Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}
