using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Data.Results;

namespace TaskTracker.Core.Features.Authentaction.Commands.Models
{
    public class RefershTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TenantId { get; set; }  // يتم ملؤه من Controller


    }
}
