using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.Users.DTOs;

namespace TaskTracker.Core.Features.Users.Query.Models
{
    public class GetUserByIdQuery : IRequest<Response<UserResponse>>
    {
        public int Id { get; set; }
        public string TenantId { get; set; }  // يتم ملؤه من Controller

        public GetUserByIdQuery(int id, string tenantId)
        {
            Id = id;
            TenantId = tenantId;
        }
    }
}
