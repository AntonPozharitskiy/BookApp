using System;
using API.Requests;
using BLL;
using BLL.Managers;
using FluentValidation;

namespace API.Validators
{
    public class UserValidator : AbstractValidator<RequestUserModel>
    {
        public UserValidator(IUserManager manager, ISignInManager signInManager)
        {
            var userManager = manager;
            RuleFor(prop => prop.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Invalid email");
            RuleFor(prop => prop.Password).NotNull().NotEmpty().MinimumLength(8).WithMessage("Password must not be null or empty and have at least 8 symbols");
        }
    }
}
