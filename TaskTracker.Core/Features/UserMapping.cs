using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Features.Users.DTOs;
using TaskTracker.Data.Entities;

namespace TaskTracker.Core.Features
{
    public partial class UserMapping : Profile
    {

            public UserMapping()
            {
            GetUserLIstMapp();

            AddUsercommanmapping();
            }
        
    }
}

