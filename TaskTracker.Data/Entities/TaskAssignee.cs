using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Data.Entities
{
    // Junction: Task <-> User (assignees)
    public class TaskAssignee
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }

        public TaskItem Task { get; set; }
        public User User { get; set; }
    }

}
