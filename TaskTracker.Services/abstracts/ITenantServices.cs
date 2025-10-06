using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;

namespace TaskTracker.Services.abstracts
{
    public interface ITenantServices
    {
        public Task<List<Tenant>> GetTenantListAsync(string tenantId);
        public Task<String> AddAsync(Tenant tenant);
    }
}
