using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;
using MediatR;
using TaskTracker.Core.Bases;

namespace TaskTracker.Core.Features.Users.Commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {

        public int TenantId { get; set; }  // يتم ملؤه من Controller
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
        
       

    }
}
