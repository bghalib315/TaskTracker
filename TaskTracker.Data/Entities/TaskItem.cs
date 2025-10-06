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
    public class TaskItem
    {
        public TaskItem()
        {
            TaskTags = new HashSet<TaskTag>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Status { get; set; }   // open, in_progress, done...
        public int Priority { get; set; }
        [ForeignKey("User")]
        public int? CreatorId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        [ForeignKey(("Team"))]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<TaskTag> TaskTags { get; set; }
        // Relations
        public ICollection<TaskAssignee> Assignees { get; set; }
        
    }
}
