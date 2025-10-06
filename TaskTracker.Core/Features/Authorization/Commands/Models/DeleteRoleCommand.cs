using MediatR;
using TaskTracker.Core.Bases;

namespace TaskTracker.Core.Features.Authorization.Commands.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string TenantId { get; set; }  // يتم ملؤه من Controller

        public DeleteRoleCommand(int id, string tenantId)
        {
            Id = id;
            TenantId = tenantId;

        }
    }
}
