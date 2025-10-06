using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskTracker.Core.Features.Authorization.Commands.Models;

namespace TaskTracker.Core.Features.Authorization.Commands.Validators
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields
        #endregion
        #region Constructors

        #endregion
        public EditRoleValidator()
        {
           
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Field is Empty")
                 .NotNull().WithMessage("Field is Required");

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Field is Empty")
                 .NotNull().WithMessage("Field is Required");
        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
