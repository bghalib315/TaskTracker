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
using TaskTracker.Core.Features.Tasks.Queries.DTOs;
using TaskTracker.Core.Features.Tasks.Queries.Models;
using TaskTracker.Core.Features.tenant.DTOs;
using TaskTracker.Core.Features.tenant.Query.Models;
using TaskTracker.Core.Features.Users.DTOs;
using TaskTracker.Core.Features.Users.Query.Models;
using TaskTracker.Core.Wrappers;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Data.Helpers;
using TaskTracker.Services.abstracts;
using TaskTracker.Services.Repository;

namespace TaskTracker.Core.Features.Tasks.Queries.Handlers
{
    public class TaskHandler : ResponseHandler, IRequestHandler<GetTaskList, Response<List<TaskResponse>>>,
                                              IRequestHandler<GetTaskByIdQuery, Response<TaskResponse>>,
                                              IRequestHandler<GetTaskPagnitedListQuery, PaginatedResult<GetTaskListPagnitedResponse>>
    {
        private readonly ITaskServices _taskServices;
        private readonly IMapper _mappper;
        private readonly ResponseHandler _responseHandler;
        public TaskHandler(ITaskServices taskServices,IMapper mapper,ResponseHandler responseHandler)
        {
            _mappper = mapper;
            _taskServices = taskServices;
            _responseHandler = responseHandler;
        }
        public async Task<Response<List<TaskResponse>>> Handle(GetTaskList request, CancellationToken cancellationToken)
        {
            var tasks =await _taskServices.GetTaskListAsync(request.TenantId);
            var response = _mappper.Map<List<TaskResponse>>(tasks);
            return _responseHandler.Success(response);

        }
        

        public async Task<PaginatedResult<GetTaskListPagnitedResponse>> Handle(GetTaskPagnitedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<TaskItem, GetTaskListPagnitedResponse>> expression = e => new GetTaskListPagnitedResponse(e.Id, e.Title, e.Status, e.Priority, e.User.UserName, e.Description,e.Tenant.Name,e.Team.Name);
            var FilterQuery = _taskServices.FilterTaskPaginatedQuerable(request.OrderBy,request.Search,request.TenantId);
            // var querable = _userServices.GetUserQuerable();
            var pagnitedlist = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return pagnitedlist;


        }
        public async Task<Response<TaskResponse>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            //var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id==request.Id);
            var user = await _taskServices.GetByIDAsync(request.Id, request.TenantId);
            if (user == null) return NotFound<TaskResponse>("NotFound");
            var result = _mappper.Map<TaskResponse>(user);
            return Success(result);
        }

        
    }
}
