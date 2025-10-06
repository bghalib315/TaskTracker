using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Data.Entities
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Identifier { get; set; } // مثل كود tenant في الـJWT أو subdomain

        public ICollection<User> Users { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
