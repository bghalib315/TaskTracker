using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;
using TaskTracker.Infrastructure.interfaces;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Services.Repository
{
    public class TenantService : ITenantServices
    {
        #region
        private readonly ITenantRepositry _tenantRepository;
        #endregion
        public TenantService(ITenantRepositry tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }


        #region Function Handles
        public async Task<List<Tenant>> GetTenantListAsync(string tenantId)
        {
            return await _tenantRepository.GetTableNoTracking(tenantId).ToListAsync();
        }
        public async Task<String> AddAsync(Tenant tenant)
        {

            //var _userresult = _taskRepository.GetTableNoTracking().Where(x => x.Username.Equals(task.Username)).FirstOrDefault();
            //if (_userresult != null) return "Exist";
            await _tenantRepository.AddAsync(tenant,tenant.Name);
            return "Success";
        }
        public async Task<string> EditAsync(Tenant _tenant)
        {
            await _tenantRepository.UpdateAsync(_tenant,_tenant.Name);
            return "Success";
        }

        public async Task<string> DeleteAsync(Tenant _tenant)
        {
            var trans = _tenantRepository.BeginTransaction();
            try
            {
                await _tenantRepository.DeleteAsync(_tenant, _tenant.Name);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                //Log.Error(ex.Message);
                return "Falied";
            }

        }
        public async Task<Tenant> GetByIDAsync(int id, string tenantId)
        {
            var _tenant = await _tenantRepository.GetByIdAsync(id,tenantId);
            return _tenant;
        }
        #endregion

    }
}
