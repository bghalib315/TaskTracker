using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;

namespace TaskTracker.Core.Features.Tasks.Commands.Models
{
    public class DeleteTaskCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string TenantId { get; set; }  // يتم ملؤه من Controller

        public DeleteTaskCommand(int id, string tenantId)
        {
            Id = id;
            TenantId = tenantId;

        }
    }
}
