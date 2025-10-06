using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Core.Features.Tasks.Commands.Models
{
    public class EditTaskCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
      
        public string Status { get; set; }   // open, in_progress, done...
        public int Priority { get; set; }  
        public string Description { get; set; }
        public string TenantId { get; set; }

    }
}
