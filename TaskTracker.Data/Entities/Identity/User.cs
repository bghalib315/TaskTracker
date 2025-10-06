using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Data.Entities.Identity
{


    public class User : IdentityUser<int>
    {
        public User()
        {
            UserrefershToken = new HashSet<UserRefershToken>();
        }

        public String Fullname { get; set; }    
        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public bool IsActive { get; set; }
        public virtual Team? Team { get; set; }
        [InverseProperty(nameof(UserRefershToken.user))]
        public virtual ICollection<UserRefershToken> UserrefershToken { get; set; }


        // Relations
        public ICollection<UserTeam> UserTeams { get; set; }
        public ICollection<TaskAssignee> TaskAssignments { get; set; }
        
    }

}
