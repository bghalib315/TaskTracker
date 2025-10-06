using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Features.Tasks.Queries.DTOs;
using TaskTracker.Core.Features.tenant.DTOs;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Core.Features.Tasks.Queries.Mapping
{
    public class TaskMapping : Profile
    {
        public TaskMapping()
        {
        
            // العكس إذا تحتاج
            CreateMap<TaskResponse, TaskItem>();
            CreateMap<TaskItem, TaskResponse>().ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name)).ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.Tenant.Name)).ForMember(dest =>dest.CreatorUserName,opt=> opt.MapFrom(src => src.User.UserName));

        }

    }
}
