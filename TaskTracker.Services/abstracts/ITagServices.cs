using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;

namespace TaskTracker.Services.abstracts
{
    public interface ITagServices 
    {
        public Task<List<Tag>> GettagListAsync(string tenantId);
        public Task<String> AddAsync(Tag tag, string tenantId);
    }
}
