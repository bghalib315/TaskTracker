using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;

namespace TaskTracker.Services.abstracts
{
    public interface ITeamServices
    {
        public Task<List<Team>> GetTeamListAsync(string tenantId);
        public Task<String> AddAsync(Team team, string tenantId);
    }
}
