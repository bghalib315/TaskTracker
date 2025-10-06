using AutoMapper;
using TaskTracker.Core.Features.Users.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TaskTracker.Core.Features.tenant.DTOs;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Data.Entities.Identity;

namespace TaskTracker.Core.Features
{
    public partial class UserMapping
    {
        public void AddUsercommanmapping()
        {
            // من الـ Entity إلى DTO
            CreateMap<AddUserCommand,User>();
            // CreateMap<User, UserResponse>();
            // العكس إذا تحتاج
           // CreateMap<UserResponse, User>();
        }
    }
}
