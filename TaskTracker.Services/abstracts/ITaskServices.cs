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
    public interface ITaskServices
    {
        public Task<List<TaskItem>> GetTaskListAsync(string tenantId);
        public Task<String> AddAsync(TaskItem task, string tenantId);
        public Task<TaskItem> GetTaskByIDWithIncludeAsync(int id);
        public Task<TaskItem> GetByIDAsync(int id, string tenantId);
        public IQueryable<TaskItem> GetTaskQuerable();
        public IQueryable<TaskItem> GetTasksByTenantIDQuerable(int ID);
        public IQueryable<TaskItem> FilterTaskPaginatedQuerable(TaskOrderingEnum taskOrdering, string search, string tenantId, bool isAscending = true);
        public Task<string> EditAsync(TaskItem task, string tenantId);
        public Task<string> DeleteAsync(TaskItem task, string tenantId);
    }
}
