using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskTracker.Core.Features.Tasks.Commands.Models;
using TaskTracker.Core.Features.Tasks.Queries.Models;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Core.Features.Users.Query.Models;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

       
        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private int GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? Convert.ToInt32(userIdClaim) : 0;
        }

        [Authorize(Roles = "Admin,Maintainer,Viewer")]
        [HttpGet("/Task/GetTaskList")]
        public async Task<IActionResult> GetTaskList()
        {
            string tenantid = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            var response = await _mediator.Send(new GetTaskList(tenantid));
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Maintainer,Viewer")]
        [HttpGet("/Task/pagnitedTask")]
        public async Task<IActionResult> pagnitedTask([FromQuery] GetTaskPagnitedListQuery query)
        {
            string tenantid = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            query.TenantId = tenantid;
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Maintainer")]
        [HttpPost("/Task/createTask")]
        public async Task<IActionResult> createTask(AddTaskCommand command)
        {
            var createrid = GetUserId();
            command.CreatorId = createrid;
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet("/Task/GetTaskByID")]
        public async Task<IActionResult> GetTaskByID([FromQuery] int id)
        {
            string tenantid = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            var response = await _mediator.Send(new GetTaskByIdQuery(id, tenantid));
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Maintainer")]
        [HttpDelete("/Task/DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            string tenantid = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            var request = await _mediator.Send(new DeleteTaskCommand(id, tenantid));
            return Ok(request);
            
        }
    }
}
