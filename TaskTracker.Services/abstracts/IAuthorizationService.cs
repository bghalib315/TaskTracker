using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.DTOs;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Services.abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName, string tenantId);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request, string tenantId);
        public Task<string> DeleteRoleAsync(int roleId, string tenantId);
        public Task<string> AddRoleToUserAsync(string roleName, string tenantId);
        //public Task<bool> IsRoleExistById(int roleId);
        //public Task<List<Role>> GetRolesList();
        //public Task<Role> GetRoleById(int id);
        //public Task<ManageUserRolesResult> ManageUserRolesData(User user);
        //public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        //public Task<ManageUserClaimsResult> ManageUserClaimData(User user);
        //public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);
    }
}
