using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Infrastructure.Bases
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task DeleteRangeAsync(ICollection<T> entities, string tenantId);
        Task<T> GetByIdAsync(int id, string tenantId);
        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking(string tenantId);
        IQueryable<T> GetTableAsTracking(string tenantId);
        Task<T> AddAsync(T entity, string tenantId);
        Task AddRangeAsync(ICollection<T> entities, string tenantId);
        Task UpdateAsync(T entity, string tenantId);
        Task UpdateRangeAsync(ICollection<T> entities, string tenantId);
        Task DeleteAsync(T entity, string tenantId);

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
    }
}
