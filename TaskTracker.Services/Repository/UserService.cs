using Azure.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Data.Helpers;
using TaskTracker.Infrastructure.Data;
using TaskTracker.Infrastructure.interfaces;
using TaskTracker.Infrastructure.Repositories;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Services.Repository
{
    public class UserService : IUserServices
    {
        #region
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepositry _teamRepository;
        private readonly ITenantRepositry _tenantRepository;
        private readonly ApplicationDBContext _applicationDBContext;
        #endregion
        public UserService(IUserRepository userRepository, ITeamRepositry teamRepositry, ITenantRepositry tenantRepository, ApplicationDBContext applicationDBContext, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepositry;
            _tenantRepository = tenantRepository;
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
        }


        public async Task<List<User>> GetUserListAsync(string tenantId)
        {
            return await _userRepository.GetUserListAsync(tenantId);
        }
        public async Task<String> AddAsync(User user, string tenantId)
        {
            var _userresult = _userRepository.GetTableNoTracking(tenantId).Where(x => x.UserName.Equals(user.UserName)).FirstOrDefault();
            var _userresult2 = _teamRepository.GetTableNoTracking(tenantId).Where(x => x.Id.Equals(user.TeamId)).FirstOrDefault();
            var _userresult3 = _tenantRepository.GetTableNoTracking(tenantId).Where(x => x.Id.Equals(user.TenantId)).FirstOrDefault();

           
          //  if (_userresult2 == null) return "Team  does not exist.";
          //else if (_userresult3 != null) return "Tenant does not exist.";
            if (_userresult != null) return "Exist";
            
            await _userRepository.AddAsync(user,tenantId);
            return "Success";
        }
        public async Task<string> EditAsync(User _user, string tenantId)
        {
            await _userRepository.UpdateAsync(_user, tenantId);
            return "Success";
        }
        public async Task<string> AddUserAsync(User user, string password, string tenantId)
        {
           // var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                //if Email is Exist
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                //email is Exist
                if (existUser != null) return "EmailIsExist";

                //if username is Exist
                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                //username is Exist
                if (userByUserName != null) return "UserNameIsExist";
                //Create
                var createResult = await _userManager.CreateAsync(user, password);

                //Failed
                if (!createResult.Succeeded)
                    return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());

                await _userManager.AddToRoleAsync(user, "Viewer");

                ////Send Confirm Email
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                ////var resquestAccessor = _httpContextAccessor.HttpContext.Request;
                ////var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                //var message = $"To Confirm Email Click Link: <a href='{returnUrl}'></a>";
                ////$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                ////message or body
                //await _emailsService.SendEmail(user.Email, message, "ConFirm Email");

                //await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
               // await trans.RollbackAsync();
                return "Failed";
            }

        }


        public async Task<string> DeleteAsync(User _user, string tenantId)
        {
            var trans = _userRepository.BeginTransaction();
            try
            {
                await _userRepository.DeleteAsync(_user, tenantId);
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
        public async Task<User> GetByIDAsync(int id, string tenantId)
        {
            var _user = await _userRepository.GetByIdAsync(id, tenantId);
            return _user;
        }

        public IQueryable<User> GetUserQuerable(string tenantId)
        {
            return _userRepository.GetTableNoTracking(tenantId).Include(x => x.Team).ThenInclude(t => t.Tenant).AsQueryable();
        }

        public IQueryable<User> GetUserssByTenantIDQuerable(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> FilterUserPaginatedQuerable(UserOrderingEnum userOrderingEnum, string search, string tenantId, bool isAscending = true) 
        {
            
         var query = _userRepository.GetTableNoTracking(tenantId).Include(x => x.Team)
                                                         .ThenInclude(t => t.Tenant)
                                                         .AsQueryable();
           
            // 🔍 البحث
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x =>
                    x.UserName.Contains(search) ||
                    x.Email.Contains(search) ||
                    x.Team.Name.Contains(search) ||
                    x.Team.Tenant.Name.Contains(search));
            }
            // 🔄 Ordering with switch expression
            query = userOrderingEnum switch
            {
                UserOrderingEnum.Id => isAscending ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id),
                UserOrderingEnum.Username => isAscending ? query.OrderBy(x => x.UserName) : query.OrderByDescending(x => x.UserName),
                UserOrderingEnum.TeamName => isAscending ? query.OrderBy(x => x.Team.Name) : query.OrderByDescending(x => x.Team.Name),
                UserOrderingEnum.TenantName => isAscending ? query.OrderBy(x => x.Tenant.Name) : query.OrderByDescending(x => x.Tenant.Name),
                UserOrderingEnum.Email => isAscending ? query.OrderBy(x => x.Email) : query.OrderByDescending(x => x.Email),
                _ => query.OrderBy(x => x.Id) // default
            };
            return query;

        }
    }
}
