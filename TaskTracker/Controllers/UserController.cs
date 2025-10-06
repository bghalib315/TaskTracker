using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Core.Features.Users.Query.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        // هنا دالة GetTenantId
        private string GetTenantId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
        }
        private int GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? Convert.ToInt32(userIdClaim) : 0;
        }

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("/User/GetUserList")]
        public async Task<IActionResult> GetUserList() {
            string tenantid = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            var response =await _mediator.Send(new GetUserList(tenantid));
            return Ok(response);
        }
        [HttpGet("/User/pagnitedUser")]
        public async Task<IActionResult> pagnitedUser(GetUserPagnitedListQuery query)
        {
            query.TenantId = GetTenantId();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpPost("/User/createUser")]
        public async Task<IActionResult> createUser(AddUserCommand command)
        {
            command.TenantId = int.Parse(GetTenantId());
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet("/User/GetUserByID")]
        public async Task<IActionResult> GetUserByID([FromQuery] int id)
        {
            string tenantid = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            var response = await _mediator.Send(new GetUserByIdQuery(id,tenantid));
            return Ok(response);
        }
    }
}
