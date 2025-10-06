using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Core.Features.Tasks.Commands.Models
{
    public class AddTaskCommand : IRequest<Response<string>>
    {
        
       
        public string Title { get; set; }
        public string Status { get; set; }   // open, in_progress, done...
        public int Priority { get; set; }
        public int? CreatorId { get; set; }
        public string Description { get; set; }
        public string TenantId { get; set; }      
        public int TeamId { get; set; }
        // ✨ الجديد
        public List<int> AssigneeIds { get; set; } = new();
    }
}
