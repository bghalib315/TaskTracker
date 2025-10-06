using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.Tasks.Queries.DTOs;

namespace TaskTracker.Core.Features.Tasks.Queries.Models
{
    public class GetTaskList : IRequest<Response<List<TaskResponse>>>
    {
        public string TenantId { get; set; }  // يتم ملؤه من Controller
        public GetTaskList(string tenantId )
        {
            TenantId = tenantId;
        }
       

    }
}
