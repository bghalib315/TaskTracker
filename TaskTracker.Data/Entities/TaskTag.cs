using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Data.Entities
{
    public class TaskTag
    {
        [Key]
        public int TaskTagId { get; set; }
        public int TaskItemId { get; set; }
        [ForeignKey("TaskItemId")]
        public virtual TaskItem TaskItem { get; set; }
        public int TagId { get; set; }
      
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
