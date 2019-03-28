using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Requests;
using BLL;
using BLL.Managers;
using FluentValidation;

namespace API.Validators
{
    public class AuthenticateUserValidator : AbstractValidator<RequestAuthorizeUserModel>
    {
        public AuthenticateUserValidator(IUserManager manager, ISignInManager signInManager)
        {
            var userManager = manager;

            RuleFor(x => x.Email).EmailAddress().NotEmpty().MustAsync(async (model, email, context) =>
            {
                var userResult = await userManager.GetUserByEmail(email);
                return userResult != null;
            }).WithMessage($"Invalid email.");

            RuleFor(x => x.Password).NotEmpty().WithMessage($"Password can't be empty")
                .MustAsync(async (model, email, context) =>
                {
                    var user = await userManager.GetUserByEmail(model.Email);
                    var passwordResult = await signInManager.CheckPassword(user, model.Password, false);
                    return passwordResult.Succeeded;
                }).WithMessage($"Password is incorrect");
        }
    }
}
