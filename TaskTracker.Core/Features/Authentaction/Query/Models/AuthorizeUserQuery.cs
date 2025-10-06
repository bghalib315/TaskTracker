using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;

namespace TaskTracker.Core.Features.Authentaction.Query.Models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
        public string TenantId { get; set; }  // يتم ملؤه من Controller

    }
}
