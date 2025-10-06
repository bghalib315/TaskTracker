using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.Tasks.Queries.DTOs;
using TaskTracker.Core.Features.Users.DTOs;

namespace TaskTracker.Core.Features.Tasks.Queries.Models
{
    public class GetTaskByIdQuery : IRequest<Response<TaskResponse>>
    {
        public int Id { get; set; }
        public string TenantId { get; set; }  // يتم ملؤه من Controller

        public GetTaskByIdQuery(int id, string tenantId)
        {
            Id = id;
            TenantId = tenantId;
        }
    }
}
