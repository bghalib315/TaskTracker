using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.tenant.DTOs;
using TaskTracker.Core.Features.tenant.Query.Models;
using TaskTracker.Core.Features.Users.DTOs;
using TaskTracker.Core.Features.Users.Query.Models;
using TaskTracker.Core.Wrappers;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Core.Features.Users.Query.Handlers
{
    public class UserHandler : ResponseHandler, IRequestHandler<GetUserList,Response<List<UserResponse>>>
                              ,IRequestHandler<GetUserPagnitedListQuery, PaginatedResult<GetUserListPagnitedResponse>>,
                               IRequestHandler<GetUserByIdQuery, Response<UserResponse>>
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mappper;
        private readonly ResponseHandler _responseHandler;

        public UserHandler(IUserServices userServices,IMapper mapper, ResponseHandler responseHandler,UserManager<User> userManager)
        {
            _userServices = userServices;
            _mappper = mapper;
            _responseHandler = responseHandler;
            _userManager = userManager;
        
        }
      

    public  async Task<Response<List<UserResponse>>> Handle(GetUserList request, CancellationToken cancellationToken)
        {
            var users = await _userServices.GetUserListAsync(request.TenantId);
          
            var response = _mappper.Map<List<UserResponse>>(users);
            return _responseHandler.Success(response);

           
        }

        public async Task<PaginatedResult<GetUserListPagnitedResponse>> Handle(GetUserPagnitedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<User, GetUserListPagnitedResponse>> expression = e => new GetUserListPagnitedResponse(e.Id,e.UserName,e.Email,e.PasswordHash,e.Team.Name,e.Tenant.Name);
            var FilterQuery = _userServices.FilterUserPaginatedQuerable(request.OrderBy,request.Search,request.TenantId);
           // var querable = _userServices.GetUserQuerable();
            var pagnitedlist =await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return pagnitedlist;


        }
        public async Task<Response<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id==request.Id);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<UserResponse>("NotFound");
            var result = _mappper.Map<UserResponse>(user);
            return Success(result);
        }
    }
}
