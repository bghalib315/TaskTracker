using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Data.Helpers
{
    public enum TaskOrderingEnum
    {
        Id = 0,
        Title = 1,
        Status = 2,
        Priority = 3,
        CreatorUserName = 4,
        Description = 5,
        TenantName = 6,
        TeamName = 7
    }
}
