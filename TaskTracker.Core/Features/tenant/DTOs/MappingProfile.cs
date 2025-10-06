using AutoMapper;
using TaskTracker.Data.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskTracker.Core.Features.tenant.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // من الـ Entity إلى DTO
          //  CreateMap<Tenant, TenantResponse>().ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name)).ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.Team.Tenant.Name));
             CreateMap<Tenant, TenantResponse>();
            // العكس إذا تحتاج
            CreateMap<TenantResponse, Tenant>();
        }
    }
}
