using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Core.Features.Authentaction.Commands.Handlers;
using TaskTracker.Core.Features.Authentaction.Commands.Models;
using TaskTracker.Core.Features.Authentaction.Query.Models;
using TaskTracker.Core.Features.Users.Commands.Models;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        // هنا دالة GetTenantId
        private string GetTenantId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
        }
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("/Authentication/create")]
        public async Task<IActionResult> create([FromForm] SignInCommand command)
        {
           
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPost("/Authentication/RefershToken")]
        public async Task<IActionResult> RefershToken([FromForm] RefershTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPost("/Authentication/ValidateToken")]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = _mediator.Send(query);
            return Ok(response);
        }
    }
}
