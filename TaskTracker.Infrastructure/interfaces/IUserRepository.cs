using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Infrastructure.Bases;

namespace TaskTracker.Infrastructure.interfaces
{
    public interface IUserRepository : IGenericRepositoryAsync<User>
    {
        public Task<List<User>> GetUserListAsync(string tenantId);
       
    }
}
