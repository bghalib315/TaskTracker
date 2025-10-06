using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Core.Features.Tasks.Queries.DTOs
{
    public class GetTaskListPagnitedResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }   // open, in_progress, done...
        public int Priority { get; set; }
        public String? CreatorUserName { get; set; }
        public string Description { get; set; }

        public string TenantName { get; set; }

        public String TeamName { get; set; }
        public GetTaskListPagnitedResponse(int id,string title,string status,int priority,string createrusername, string description, string tenantName,String teamName )
        {
            Id = id;
            Title = title;
            Status = status;    
            Priority = priority;
            CreatorUserName = createrusername;
            Description = description;
            TenantName = tenantName;
            TeamName = teamName;       
        }
    }
}
