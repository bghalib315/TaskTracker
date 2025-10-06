using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;

namespace TaskTracker.Core.Features.tenant.Commands.Models
{
    public class AddTenantCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Identifier { get; set; }

    }
}
