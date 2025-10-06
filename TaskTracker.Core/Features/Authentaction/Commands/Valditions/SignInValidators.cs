using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Features.Authentaction.Commands.Models;

namespace TaskTracker.Core.Features.Authentaction.Commands.Valditions
{
    public class SignInValidators : AbstractValidator<SignInCommand>
    {
        #region Constructors
        public SignInValidators()
        {
           
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserName)
                 .NotEmpty().WithMessage("Username cannot be empty.")
                 .NotNull().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password is required.");
        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion
    }
}
