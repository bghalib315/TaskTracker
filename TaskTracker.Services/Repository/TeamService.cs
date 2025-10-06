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
    public class TeamService : ITeamServices
    {
        #region
        private readonly ITeamRepositry _teamRepository;
        #endregion
        #region constactor
        public TeamService(ITeamRepositry teamRepository)
        {
            _teamRepository = teamRepository;
        }
        #endregion

        #region Function Handles
        public async Task<List<Team>> GetTeamListAsync(string tenantId)
        {
            return await _teamRepository.GetTableNoTracking(tenantId).ToListAsync();
        }
        public async Task<String> AddAsync(Team team, string tenantId)
        {

            //var _userresult = _taskRepository.GetTableNoTracking().Where(x => x.Username.Equals(task.Username)).FirstOrDefault();
            //if (_userresult != null) return "Exist";
            await _teamRepository.AddAsync(team, tenantId);
            return "Success";
        }
        public async Task<string> EditAsync(Team _team, string tenantId)
        {
            await _teamRepository.UpdateAsync(_team, tenantId);
            return "Success";
        }

        public async Task<string> DeleteAsync(Team _team, string tenantId)
        {
            var trans = _teamRepository.BeginTransaction();
            try
            {
                await _teamRepository.DeleteAsync(_team, tenantId);
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
        public async Task<Team> GetByIDAsync(int id, string tenantId)
        {
            var _team = await _teamRepository.GetByIdAsync(id, tenantId);
            return _team;
        }
        #endregion

    }
}
