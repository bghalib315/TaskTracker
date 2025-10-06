using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Data.Helpers;
using TaskTracker.Data.Results;
using TaskTracker.Infrastructure.Data;
using TaskTracker.Infrastructure.interfaces;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Services.Repository
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly jwtSettings _jwtSettings;
         private readonly IRefershTokenRepositry _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
      //  private readonly IEmailsService _emailsService;
        private readonly ApplicationDBContext _applicationDBContext;
      //  private readonly IEncryptionProvider _encryptionProvider;
        #endregion 

        #region Constructors
        public AuthenticationService(jwtSettings jwtOptions,
                                    IRefershTokenRepositry refreshTokenRepository,                           
                                     UserManager<User> userManager,
                                    // IEmailsService emailsService,
                                     ApplicationDBContext applicationDBContext)
        {
            _jwtSettings = jwtOptions;
              _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
           // _emailsService = emailsService;
            _applicationDBContext = applicationDBContext;
           // _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }


        #endregion

        #region Handle Functions

        public  async Task<JwtAuthResult> GetJWTToken(User user)
        {
           
        var (jwtToken,accessToken) =await GenerateJWTToken(user);
               var refershToken= await    GetRefreshToken(user.UserName);
              var userRefershtoken=new UserRefershToken
              {
                  AddedTime = DateTime.UtcNow,
                  ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                  IsRevoked = false,
                  IsUsed = true,
                  JwtId = jwtToken.Id,
                  RefreshToken =refershToken.TokenString,
                  Token=accessToken,
                  UserId = user.Id
              };

         await   _refreshTokenRepository.AddAsync(userRefershtoken,user.TenantId.ToString());
            var response = new JwtAuthResult();
            response.refreshToken= refershToken;
            response.AccessToken=accessToken;
            return  response;
        }
        private async Task<RefreshToken> GetRefreshToken(String Username)
        {
            var refershToken = new RefreshToken
            {
                ExpireAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = Username,
                TokenString = GenerateRefershToken()
            };
             return refershToken;

        }

        private string GenerateRefershToken()
        {
            var randomnumber = new byte[32]; 
            var randomnumbergenrator =RandomNumberGenerator.Create();
            randomnumbergenrator.GetBytes(randomnumber);
            return Convert.ToBase64String(randomnumber);
        }


        private  async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var roles= await _userManager.GetRolesAsync(user);
            var claims = await GetClaims(user,roles.ToList());
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return  (jwtToken, accessToken);
        }

        public async Task<List<Claim>> GetClaims(User user,List<string> _roles)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim("tenantId", user.TenantId.ToString()) // tenantId
            };
            foreach (var role in _roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        public Task<JwtAuthResult> GetRefreshToken(string AccessToken, string refreshToken)
        {

            
            throw new NotImplementedException();
        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
            response.refreshToken = refreshTokenResult;
            return response;

        }

       

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifetime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        

    public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return  ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }
            
          var user= jwtToken.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
           
            //    //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking(user)
                                             .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                     x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken,user);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpiryDate;
            return (userId, expirydate);
        }

        //public async Task<string> ConfirmEmail(int? userId, string? code)
        //{
        //    if (userId == null || code == null)
        //        return "ErrorWhenConfirmEmail";
        //    var user = await _userManager.FindByIdAsync(userId.ToString());
        //    var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
        //    if (!confirmEmail.Succeeded)
        //        return "ErrorWhenConfirmEmail";
        //    return "Success";
        //}

        //public async Task<string> SendResetPasswordCode(string Email)
        //{
        //    var trans = await _applicationDBContext.Database.BeginTransactionAsync();
        //    try
        //    {
        //        //user
        //        var user = await _userManager.FindByEmailAsync(Email);
        //        //user not Exist => not found
        //        if (user == null)
        //            return "UserNotFound";
        //        //Generate Random Number

        //        //Random generator = new Random();
        //        //string randomNumber = generator.Next(0, 1000000).ToString("D6");
        //        var chars = "0123456789";
        //        var random = new Random();
        //        var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

        //        //update User In Database Code
        //        user.Code = randomNumber;
        //        var updateResult = await _userManager.UpdateAsync(user);
        //        if (!updateResult.Succeeded)
        //            return "ErrorInUpdateUser";
        //        var message = "Code To Reset Passsword : " + user.Code;
        //        //Send Code To  Email 
        //        await _emailsService.SendEmail(user.Email, message, "Reset Password");
        //        await trans.CommitAsync();
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        await trans.RollbackAsync();
        //        return "Failed";
        //    }
        //}

        //public async Task<string> ConfirmResetPassword(string Code, string Email)
        //{
        //    //Get User
        //    //user
        //    var user = await _userManager.FindByEmailAsync(Email);
        //    //user not Exist => not found
        //    if (user == null)
        //        return "UserNotFound";
        //    //Decrept Code From Database User Code
        //    var userCode = user.Code;
        //    //Equal With Code
        //    if (userCode == Code) return "Success";
        //    return "Failed";
        //}

        //public async Task<string> ResetPassword(string Email, string Password)
        //{
        //    var trans = await _applicationDBContext.Database.BeginTransactionAsync();
        //    try
        //    {
        //        //Get User
        //        var user = await _userManager.FindByEmailAsync(Email);
        //        //user not Exist => not found
        //        if (user == null)
        //            return "UserNotFound";
        //        await _userManager.RemovePasswordAsync(user);
        //        if (!await _userManager.HasPasswordAsync(user))
        //        {
        //            await _userManager.AddPasswordAsync(user, Password);
        //        }
        //        await trans.CommitAsync();
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        await trans.RollbackAsync();
        //        return "Failed";
        //    }
        //}

        #endregion
    }
}
