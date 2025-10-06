using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Data.Helpers;

namespace TaskTracker.Services.abstracts
{
    public interface IUserServices
{
        public Task<List<User>> GetUserListAsync(string tenantId);
        public Task<String> AddAsync(User user, string tenantId);
        public IQueryable<User> GetUserQuerable(string tenantId);
        public IQueryable<User> GetUserssByTenantIDQuerable(int ID);
        public IQueryable<User> FilterUserPaginatedQuerable(UserOrderingEnum userOrderingEnum,string search, string tenantId, bool isAscending = true);
        public Task<string> AddUserAsync(User user, string password, string tenantId);
        


    }
}
