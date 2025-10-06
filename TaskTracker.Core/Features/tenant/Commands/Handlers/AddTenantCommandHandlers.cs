using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.tenant.Commands.Models;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Services.abstracts;
using TaskTracker.Services.Repository;

namespace TaskTracker.Core.Features.tenant.Commands.Handlers
{
    public class AddTenantCommandHandlers : ResponseHandler, IRequestHandler<AddTenantCommand, Response<String>>
    {
        #region Fields
        private readonly ITenantServices _tenantServices;
        private readonly IMapper _mapper;
        #endregion
        #region Constarctor
        public AddTenantCommandHandlers(ITenantServices tenantServices, IMapper mapper)
        {
            _mapper = mapper;
            _tenantServices = tenantServices;
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
        public async Task<Response<string>> Handle(AddTenantCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<Tenant>(request);
            //Create
            var createResult = await _tenantServices.AddAsync(identityUser);
            switch (createResult)
            {
                
                case "Success": return Success<string>("Success");
                default: return BadRequest<string>(createResult);
            }
        }
        #endregion


    }
}
