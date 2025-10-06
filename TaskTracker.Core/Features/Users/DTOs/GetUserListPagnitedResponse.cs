using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Core.Features.Users.DTOs
{
    public class GetUserListPagnitedResponse
    {
       
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string TeamName { get; set; }
        public string TenantName { get; set; }
        public GetUserListPagnitedResponse(int id, String name, String email, String password, String teamName, String tenantname)
        {
            Id = id;
            Username = name;
            Email = email;
            PasswordHash = password;
            TeamName = teamName;
            TenantName = tenantname;
        }
    }
}
