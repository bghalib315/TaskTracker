using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.Authorization.Commands.Models;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandlers : ResponseHandler,
                 IRequestHandler<AddRoleCommand,Response<string>>,
                 IRequestHandler<EditRoleCommand, Response<string>>,
                 IRequestHandler<DeleteRoleCommand, Response<string>>,
                  IRequestHandler<AddRoleToUser, Response<string>>
    {
        #region MyRegion
      
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        #endregion
        #region MyRegion
        public RoleCommandHandlers(IAuthorizationService authorizationService,UserManager<User> userManager,RoleManager<Role> roleManager) 
        {
          
            _authorizationService = authorizationService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #endregion
        #region MyRegion
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName, request.TenantId);
            if (result == "Success") return Success("");
            return BadRequest<string>("Faild to Add");
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request, request.TenantId);
            if (result == "notFound") return NotFound<string>();
            else if (result == "Success") return Success("");
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id, request.TenantId);
            if (result == "NotFound") return NotFound<string>("Field is NotFound");
            else if (result == "Used") return BadRequest<string>("Field is Used");
            else if (result == "Success") return Success("Success");
            else
                return BadRequest<string>(result);
        }
        public async Task<Response<string>> Handle(AddRoleToUser request, CancellationToken cancellationToken)
        {
            // 1. جلب المستخدم
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new Response<string>("User not found");

            // 2. تحقق من وجود الدور
            var roleExists = await _roleManager.FindByNameAsync(request.RoleName);
            if (roleExists == null)
                return new Response<string>("Role not found");

            // 3. تحقق إذا كان المستخدم يمتلك الدور بالفعل
            var hasRole = await _userManager.IsInRoleAsync(user, request.RoleName);
            if (hasRole)
                return new Response<string>("User already has this role");

            // 4. إضافة الدور للمستخدم
            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            if (!result.Succeeded)
                return new Response<string>("Failed to add role");

            return new Response<string>("Role added successfully");
        }
        //public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        //{
        //    var result = await _authorizationService.UpdateUserRoles(request);
        //    switch (result)
        //    {
        //        case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
        //        case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
        //        case "FailedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
        //        case "FailedToUpdateUserRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
        //    }
        //    return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        //}
        #endregion
    }

}
