using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Core.Features.Users.DTOs;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandlers : ResponseHandler, IRequestHandler<AddUserCommand, Response<String>>
    {
        #region Fields
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _usermanager;

        #endregion
        #region Constarctor
        public UserCommandHandlers(IUserServices userServices, IMapper mapper, UserManager<User> userManager)
        {
            _userServices = userServices;
            _mapper = mapper;
            _usermanager = userManager;
        }
        #endregion
        #region Handles Functions
        //public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        //{

        //    var Usermapper = _mapper.Map<User>(request);
        //    var result = await _userServices.AddAsync(Usermapper);
        //    if (result == "Team with Id does not exist.") return UnprocessableEntity<string>("Team with Id does not exist.");
        //    else if (result == "Exist") return UnprocessableEntity<string>("Name Is Exist");

        //    else if (result == "Team with Id does not exist.") return UnprocessableEntity<string>("Tenant with Id does not exist.");
        //    else if (result == "Success") return Created<string>("Added Successfully");
        //    else return BadRequest<string>();
        //}
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<User>(request);
         
            //Create
            var createResult = await _userServices.AddUserAsync(identityUser, request.PasswordHash,request.TenantId.ToString());
            switch (createResult)
            {
                case "EmailIsExist": return BadRequest<string>("EmailIsExist");
                case "UserNameIsExist": return BadRequest<string>("UserNameIsExist");
                case "ErrorInCreateUser": return BadRequest<string>("ErrorInCreateUser");
                case "Failed": return BadRequest<string>("Failed");
                case "Success": return Success<string>("Success");
                default: return BadRequest<string>(createResult);
            }
        }
        #endregion


    }
}
