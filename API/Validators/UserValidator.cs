using System;
using API.Requests;
using FluentValidation;

namespace API.Validators
{
    public class UserValidator : AbstractValidator<RequestUserModel>
    {
        public UserValidator()
        {
            RuleFor(prop => prop.Email).NotNull().NotEmpty().EmailAddress().WithMessage(String.Format("E-Mail field must ${0} and ${1}", "not be null, not empty", "be similar as \"example@gmail.com\""));
        }
    }
}
