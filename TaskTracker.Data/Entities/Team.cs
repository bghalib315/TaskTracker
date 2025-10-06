using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Data.Entities
{
    public class Team
    {
        public Team()
        {
            UserTeams = new HashSet<UserTeam>();
            Tasks = new HashSet<TaskItem>();
            
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        // Relations
        public  ICollection<UserTeam> UserTeams { get; set; }
        public  ICollection<TaskItem> Tasks { get; set; }
    }
}
