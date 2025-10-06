using MediatR;
using TaskTracker.Core.Bases;
using TaskTracker.Data.DTOs;

namespace TaskTracker.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {
        public string TenantId { get; set; }  // يتم ملؤه من Controller

    }
}
