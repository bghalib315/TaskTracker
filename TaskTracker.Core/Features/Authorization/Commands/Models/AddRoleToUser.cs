using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;

namespace TaskTracker.Core.Features.Authorization.Commands.Models
{
    public class AddRoleToUser : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public String RoleName { get; set; }
        public string TenantId { get; set; }  // يتم ملؤه من Controller
    }
}
