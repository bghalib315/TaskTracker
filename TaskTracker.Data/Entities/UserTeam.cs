using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Data.Entities
{
    // Junction: User <-> Team
    public class UserTeam
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public string RoleInTeam { get; set; }

        public User User { get; set; }
        public Team Team { get; set; }
    }

}
