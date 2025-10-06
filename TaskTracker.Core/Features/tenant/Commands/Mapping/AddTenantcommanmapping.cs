using AutoMapper;
using TaskTracker.Core.Features.Users.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TaskTracker.Core.Features.tenant.DTOs;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Core.Features.tenant.Commands.Models;
using TaskTracker.Data.Entities;

namespace TaskTracker.Core.Features
{
    public  class TenantMapping : Profile
    {
        public TenantMapping()
        {
            AddTenantcommanmapping();
        }
        public void AddTenantcommanmapping()
        {
            // من الـ Entity إلى DTO
            CreateMap<AddTenantCommand,Tenant>();
            // CreateMap<User, UserResponse>();
            // العكس إذا تحتاج
           // CreateMap<UserResponse, User>();
        }
    }
}
