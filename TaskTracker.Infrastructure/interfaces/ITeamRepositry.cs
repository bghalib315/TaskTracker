using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;
using TaskTracker.Infrastructure.Bases;

namespace TaskTracker.Infrastructure.interfaces
{
    public interface ITeamRepositry : IGenericRepositoryAsync<Team>
    {
    }
}
