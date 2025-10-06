using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Data.Entities;

namespace TaskTracker.Core.Features.Tasks.Queries.DTOs
{
    public class TaskResponse 
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
  
        public string Status { get; set; }   // open, in_progress, done...
        public int Priority { get; set; }
        public String? CreatorUserName { get; set; }
        public string Description { get; set; }
        
        public string TenantName { get; set; }
        
        public String TeamName { get; set; }
    }
}
