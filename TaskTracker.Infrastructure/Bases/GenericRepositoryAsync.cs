using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Infrastructure.Data;
using Azure.Core;

namespace TaskTracker.Infrastructure.Bases
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        #region Vars / Props

        private readonly ApplicationDBContext _ApplicationDBContext;

        #endregion

        #region Constructor(s)
        public GenericRepositoryAsync(ApplicationDBContext dbContext)
        {
            _ApplicationDBContext = dbContext;
        }

        #endregion


        #region Methods

        #endregion

        #region Actions
        public virtual async Task<T> GetByIdAsync(int id, string tenantId)
        {
 

         //   return await _ApplicationDBContext.Set<T>().FindAsync(id);

            var entity = await _ApplicationDBContext.Set<T>().FindAsync(id);
            if (entity == null) return null;

            // تحقق إذا كان هناك خاصية TenantId
            var tenantProperty = entity.GetType().GetProperty("TenantId");
            if (tenantProperty != null)
            {
                var value = tenantProperty.GetValue(entity)?.ToString();
                if (value != tenantId) return null; // منع الوصول لبيانات تينانت آخر
            }

            return entity;

        }


        public IQueryable<T> GetTableNoTracking(string tenantId)
        {
            // return _ApplicationDBContext.Set<T>().AsNoTracking().AsQueryable();
            var table = _ApplicationDBContext.Set<T>().AsNoTracking().AsQueryable();

            var tenantProperty = typeof(T).GetProperty("TenantId");
            if (tenantProperty != null)
            {
                table = table.Where(e => EF.Property<int>(e, "TenantId") == int.Parse(tenantId));
            }

            return table;
        }


        public virtual async Task AddRangeAsync(ICollection<T> entities, string tenantId)
        {
            await _ApplicationDBContext.Set<T>().AddRangeAsync(entities);



            await _ApplicationDBContext.SaveChangesAsync();

        }
        public virtual async Task<T> AddAsync(T entity, string tenantId)
        {
            await _ApplicationDBContext.Set<T>().AddAsync(entity);
            await _ApplicationDBContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity, string tenantId)
        {
            _ApplicationDBContext.Set<T>().Update(entity);
            await _ApplicationDBContext.SaveChangesAsync();

        }

        public virtual async Task DeleteAsync(T entity, string tenantId)
        {
            _ApplicationDBContext.Set<T>().Remove(entity);
            await _ApplicationDBContext.SaveChangesAsync();
        }
        public virtual async Task DeleteRangeAsync(ICollection<T> entities, string tenantId)
        {
            foreach (var entity in entities)
            {
                _ApplicationDBContext.Entry(entity).State = EntityState.Deleted;
            }
            await _ApplicationDBContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _ApplicationDBContext.SaveChangesAsync();
        }



        public IDbContextTransaction BeginTransaction()
        {
            return _ApplicationDBContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _ApplicationDBContext.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _ApplicationDBContext.Database.RollbackTransaction();
        }

        public IQueryable<T> GetTableAsTracking(string tenantId)
        {

            //return _ApplicationDBContext.Set<T>().AsQueryable();
            var table = _ApplicationDBContext.Set<T>().AsQueryable();

            var tenantProperty = typeof(T).GetProperty("TenantId");
            if (tenantProperty != null)
            {
                table = table.Where(e => EF.Property<string>(e, "TenantId") == tenantId);
            }

            return table;

        }

        public virtual async Task UpdateRangeAsync(ICollection<T> entities, string tenantId)
        {
            _ApplicationDBContext.Set<T>().UpdateRange(entities);
            await _ApplicationDBContext.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _ApplicationDBContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _ApplicationDBContext.Database.CommitTransactionAsync();
        }

        public async Task RollBackAsync()
        {
            await _ApplicationDBContext.Database.RollbackTransactionAsync();
        }
        #endregion

    }
}
