using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Infrastructure.Bases;
using TaskTracker.Infrastructure.Data;
using TaskTracker.Infrastructure.interfaces;

namespace TaskTracker.Infrastructure.Repositories
{
    public class UserRepositry : GenericRepositoryAsync<User>, IUserRepository
    {
        #region Fileds
        private readonly ApplicationDBContext _ApplicationDBContext;
        #endregion
        #region Constructors
        //public UserRepositry(ApplicationDBContext applicationDBContext)
        //{
        //    _ApplicationDBContext = applicationDBContext;
        //}
        public UserRepositry(ApplicationDBContext dbContext) : base(dbContext)
        {
            _ApplicationDBContext = dbContext;
        }
        #endregion
        #region Handles Functions
        public Task<List<User>> GetUserListAsync(string tenantId)
        {
            return _ApplicationDBContext.Users.Where(u => u.TenantId.ToString() == tenantId).Include(x => x.Team).ThenInclude(t => t.Tenant).ToListAsync();
        }
        #endregion
    }
}
