using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.Authentaction.Commands.Models;
using TaskTracker.Core.Features.Users.Commands.Models;
using TaskTracker.Data.Entities.Identity;
using TaskTracker.Data.Results;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Core.Features.Authentaction.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<JwtAuthResult>>, IRequestHandler<RefershTokenCommand, Response<JwtAuthResult>>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion
        #region Constructors
        public AuthenticationCommandHandler(
                                            UserManager<User> userManager,
                                            SignInManager<User> signInManager,
                                            IAuthenticationService authenticationService) 
        {
           
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }


        #endregion
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user =await  _userManager.FindByNameAsync(request.UserName);
            //Return The UserName Not Found
            if (user == null) return BadRequest<JwtAuthResult>("The UserName Not Found");
            //try To Sign in 
            var signInResult =  _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //if Failed Return Passord is wrong
            if (!signInResult.IsCompletedSuccessfully) return BadRequest<JwtAuthResult>("Failed Return Passord is wrong");
            ////confirm email
            //if (!user.EmailConfirmed)
            //    return BadRequest<string>("The UserName Not Found");
            //Generate Token
            var result = await _authenticationService.GetJWTToken(user);
            //return Token 
            return Success(result);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefershTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>("Algorithm Is Wrong");
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>("Token Is Not Expired");
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>("RefreshToken IsNot Found");
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>("RefreshToken Is Expired");
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }
    }
}
