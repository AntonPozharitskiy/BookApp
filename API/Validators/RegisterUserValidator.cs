using System;
using API.Requests;
using BLL;
using BLL.Managers;
using FluentValidation;

namespace API.Validators
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserModel>
    {
        public RegisterUserValidator(IUserManager manager, ISignInManager signInManager)
        {
            var userManager = manager;
            RuleFor(prop => prop.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Invalid email").MustAsync(async (model, email, context) =>
            {
                var userResult = await userManager.GetUserByEmail(email);
                return userResult == null;
            }).WithMessage("This Email already taken");
            RuleFor(prop => prop.Password).NotNull().NotEmpty().MinimumLength(8).WithMessage("Password must not be null or empty and have at least 8 symbols");
        }
    }
}
