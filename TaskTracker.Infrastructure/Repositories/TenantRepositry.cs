using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;
using TaskTracker.Infrastructure.Bases;
using TaskTracker.Infrastructure.Data;
using TaskTracker.Infrastructure.interfaces;

namespace TaskTracker.Infrastructure.Repositories
{
    public class TenantRepositry : GenericRepositoryAsync<Tenant>,ITenantRepositry
    {
        #region Fileds
        private readonly ApplicationDBContext _ApplicationDBContext;
        #endregion
        #region Constructors
        //public UserRepositry(ApplicationDBContext applicationDBContext)
        //{
        //    _ApplicationDBContext = applicationDBContext;
        //}
        public TenantRepositry(ApplicationDBContext dbContext) : base(dbContext)
        {
            _ApplicationDBContext = dbContext;
        }
        #endregion
        #region Handles Functions
        public Task<List<Tenant>> GetTenantsListAsync()
        {
            return _ApplicationDBContext.Tenant.ToListAsync();
        }
        #endregion
    }
}
