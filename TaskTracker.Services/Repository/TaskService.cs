using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Helpers;
using TaskTracker.Infrastructure.interfaces;
using TaskTracker.Infrastructure.Repositories;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Services.Repository
{
    public class TaskService : ITaskServices
    {
        #region fields
        private readonly ITaskRepositry _taskRepository;
        #endregion
        #region constractor
        public TaskService(ITaskRepositry taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion
        #region Function Handles
        public async Task<List<TaskItem>> GetTaskListAsync(string tenantId)
        {
            return await _taskRepository.GetTableNoTracking(tenantId).ToListAsync();
        }
        public async Task<String> AddAsync(TaskItem task, string tenantId)
        {

            //var _userresult = _taskRepository.GetTableNoTracking().Where(x => x.Username.Equals(task.Username)).FirstOrDefault();
            //if (_userresult != null) return "Exist";
            await _taskRepository.AddAsync(task, tenantId);
            return "Success";
        }
        public async Task<string> EditAsync(TaskItem _task, string tenantId)
        {
            await _taskRepository.UpdateAsync(_task, tenantId);
            return "Success";
        }

        public async Task<string> DeleteAsync(TaskItem _task, string tenantId)
        {
            var trans = _taskRepository.BeginTransaction();
            try
            {
                await _taskRepository.DeleteAsync(_task, tenantId);
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
        public async Task<TaskItem> GetByIDAsync(int id, string tenantId)
        {
            var _task = await _taskRepository.GetByIdAsync(id,tenantId);
            return _task;
        }

        public async Task<TaskItem> GetTaskByIDWithIncludeAsync(int id, string tenantId)
        {
            // var student = await _studentRepository.GetByIdAsync(id);
            var Tasks = _taskRepository.GetTableNoTracking(tenantId)
                                          .Include(x => x.CreatorId)
                                          .Where(x => x.Id.Equals(id))
                                          .FirstOrDefault();
            return Tasks;
        }

        public IQueryable<TaskItem> GetTaskQuerable()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TaskItem> GetTasksByTenantIDQuerable(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TaskItem> FilterTaskPaginatedQuerable(TaskOrderingEnum taskOrdering, string search,string tenantId, bool isAscending = true)
        {
            var query = _taskRepository.GetTableNoTracking(tenantId).Include(x => x.Team).ThenInclude(t => t.Tenant).AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Title.Contains(search) ||
                x.Status.Contains(search) ||
                x.Title.Contains(search) ||
                x.Team.Name.Contains(search) ||
                x.User.Fullname.Contains(search) ||
                x.User.UserName.Contains(search));
            }
            // 🔄 Ordering with switch expression
            query = taskOrdering switch
            {
                TaskOrderingEnum.Id=> isAscending ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id),
                TaskOrderingEnum.Status => isAscending ? query.OrderBy(x => x.Status) : query.OrderByDescending(x => x.Status),
                TaskOrderingEnum.TeamName => isAscending ? query.OrderBy(x => x.Team.Name) : query.OrderByDescending(x => x.Team.Name),
                TaskOrderingEnum.TenantName => isAscending ? query.OrderBy(x => x.Tenant.Name) : query.OrderByDescending(x => x.Tenant.Name),
                TaskOrderingEnum.CreatorUserName => isAscending ? query.OrderBy(x => x.User.UserName) : query.OrderByDescending(x => x.User.UserName),
                TaskOrderingEnum.Priority => isAscending ? query.OrderBy(x => x.Priority) : query.OrderByDescending(x => x.Priority),
                TaskOrderingEnum.Title => isAscending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title),
                _ => query.OrderBy(x => x.Id) // default
            };
            return query;
        }

        public Task<TaskItem> GetTaskByIDWithIncludeAsync(int id)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
