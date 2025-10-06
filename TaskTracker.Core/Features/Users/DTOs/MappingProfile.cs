using AutoMapper;
using TaskTracker.Core.Features.Users.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TaskTracker.Core.Features.tenant.DTOs;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Core.Features
{
    public partial class UserMapping
    {
        public void GetUserLIstMapp()
        {

            // من الـ Entity إلى DTO
            CreateMap<User, UserResponse>().ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name)).ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.Team.Tenant.Name));
            // CreateMap<User, UserResponse>();
            // العكس إذا تحتاج
            CreateMap<UserResponse, User>();

        }
    }
}
