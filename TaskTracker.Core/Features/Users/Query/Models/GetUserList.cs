using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.tenant.DTOs;
using TaskTracker.Core.Features.Users.DTOs;

namespace TaskTracker.Core.Features.Users.Query.Models
{
    public class GetUserList : IRequest<Response<List<UserResponse>>>
    {
        public string TenantId { get; set; }  // يتم ملؤه من Controller
        public GetUserList(string tenantId)
        {

            TenantId = tenantId;

        }


    }
}
