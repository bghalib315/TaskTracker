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
    public class TagService : ITagServices
    {
        #region
        private readonly ITagRepositry _TagRepository;
        #endregion
        #region constractor
        public TagService(ITagRepositry tagRepository)
        {
            _TagRepository = tagRepository;
        }
        #endregion

        #region Function Handles
        public Task<List<Tag>> GettagListAsync(string tenantId)
        {
            return  _TagRepository.GetTableAsTracking(tenantId).ToListAsync();
        }
        
        public async Task<String> AddAsync(Tag _tag, string tenantId)
        {

            //var _tagresult = _TagRepository.GetTableNoTracking().Where(x => x.Username.Equals(user.Username)).FirstOrDefault();
            //if (_userresult != null) return "Exist";
            await _TagRepository.AddAsync(_tag, tenantId);
            return "Success";
        }
        public async Task<string> EditAsync(Tag _tag, string tenantId)
        {
            await _TagRepository.UpdateAsync(_tag, tenantId);
            return "Success";
        }

        public async Task<string> DeleteAsync(Tag _tag, string tenantId)
        {
            var trans = _TagRepository.BeginTransaction();
            try
            {
                await _TagRepository.DeleteAsync(_tag, tenantId);
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
        public async Task<Tag> GetByIDAsync(int id, string tenantId)
        {
            var _tag = await _TagRepository.GetByIdAsync(id, tenantId);
            return _tag;
        }

        #endregion
    }
}
