using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Features.Tasks.Commands.Models;
using TaskTracker.Data.Entities;

namespace TaskTracker.Core.Features.Tasks.Commands.Mapping
{
    public class AddTaskCommandMapping : Profile
    {
        public AddTaskCommandMapping()
        {
            CreateMap<AddTaskCommand, TaskItem>()
    .ForMember(dest => dest.Assignees, opt => opt.Ignore());

        }
    }
}
