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
    public class RefershTokenRepositry : GenericRepositoryAsync<UserRefershToken>,IRefershTokenRepositry
    {
        #region Fields
        private DbSet<UserRefershToken> userRefreshToken;
        #endregion

        #region Constructors
        public RefershTokenRepositry(ApplicationDBContext dbContext) : base(dbContext)
        {
            userRefreshToken = dbContext.Set<UserRefershToken>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}
