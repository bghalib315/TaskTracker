using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.Authorization.Commands.Models;
using TaskTracker.Core.Features.Tasks.Commands.Models;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Core.Features.Tasks.Commands.Handlers
{
    public class AddTaskCommandHandlers : ResponseHandler, 
                                                          IRequestHandler<AddTaskCommand, Response<String>>,
                                                           IRequestHandler<EditTaskCommand, Response<String>>,
                                                            IRequestHandler<DeleteTaskCommand, Response<String>>
    {
        #region Fields
        private readonly ITaskServices _taskServices;
        private readonly IMapper _mapper;

        #endregion
        #region Constarctor
        public AddTaskCommandHandlers(ITaskServices taskServices, IMapper mapper)
        {
            _taskServices = taskServices;
            _mapper = mapper;
        }
        #endregion
        #region Handles Functions
        
        public async Task<Response<string>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
           

            // 1. تحويل AddTaskCommand -> TaskItem
            var taskEntity = _mapper.Map<TaskItem>(request);

            // 2. إضافة Assignees
            if (request.AssigneeIds != null && request.AssigneeIds.Any())
            {
                taskEntity.Assignees = request.AssigneeIds
                    .Select(userId => new TaskAssignee
                    {
                        UserId = userId,
                        Task = taskEntity
                    })
                    .ToList();
            }
            // 3. استدعاء الخدمة
            var result = await _taskServices.AddAsync(taskEntity, request.TenantId);

            // 4. إرجاع النتيجة

            if (result == "Team with Id does not exist.") return UnprocessableEntity<string>("Team with Id does not exist.");
            else if (result == "Exist") return UnprocessableEntity<string>("Name Is Exist");

            else if (result == "Team with Id does not exist.") return UnprocessableEntity<string>("Tenant with Id does not exist.");
            else if (result == "Success") return Created<string>("Added Successfully");
            else return BadRequest<string>();
            //Create

        }
        public async Task<Response<string>> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            //Check if the Id is Exist Or not
            var student = await _taskServices.GetByIDAsync(request.Id,request.TenantId);
            //return NotFound
            if (student == null) return NotFound<string>();
            //mapping Between request and student
            var studentmapper = _mapper.Map(request, student);
            //Call service that make Edit
            var result = await _taskServices.EditAsync(studentmapper,request.TenantId);
            //return response
            if (result == "Success") return Success("Success");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            //Check if the Id is Exist Or not
            var student = await _taskServices.GetByIDAsync(request.Id,request.TenantId);
            //return NotFound
            if (student == null) return NotFound<string>();
            //Call service that make Delete
            var result = await _taskServices.DeleteAsync(student, request.TenantId);
            if (result == "Success") return Deleted<string>();
            else return BadRequest<string>();
        }
        #endregion


    }
}
