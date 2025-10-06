using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Core.Features.Authentaction.Commands.Models;
using TaskTracker.Core.Features.Authorization.Commands.Models;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;
        // هنا دالة GetTenantId
        private string GetTenantId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
        }
        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("/Authorization/create")]
        public async Task<IActionResult> create([FromForm] AddRoleCommand command)
        {
            command.TenantId = GetTenantId();
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut("/Authorization/Edit")]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
        {
            command.TenantId = GetTenantId();
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete("/Authorization/Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id, string tenantId)
        {

            var response = await _mediator.Send(new DeleteRoleCommand(id,tenantId));
            return Ok(response);
        }
        [HttpPost("/Authorization/AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser([FromForm] AddRoleToUser command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        //public async Task<IActionResult> GetRoleList()
        //{
        //    var response = await Mediator.Send(new GetRolesListQuery());
        //    return NewResult(response);
        //}
    }
}
