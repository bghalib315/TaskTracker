using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Core.Features.tenant.Commands.Models;
using TaskTracker.Core.Features.tenant.Query.Models;
using TaskTracker.Core.Features.Users.Query.Models;
using TaskTracker.Data.Entities;

namespace TaskTracker.API.Controllers
{
   
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class TenantController : ControllerBase
    {
        private readonly IMediator _mediator;
        // هنا دالة GetTenantId

        public TenantController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/Tenant/GetListTenant")]
        public async Task<IActionResult> GetTenantList()
        {
            var response = await _mediator.Send(new GetTenantListQuery());
            return Ok(response);
        }
        [HttpPost("/Tenant/CreateTenant")]
        public async Task<IActionResult> CreateTenan(AddTenantCommand tenant)
        {
            var response = await _mediator.Send(tenant);
            return Ok(response);
        }
    }
}
