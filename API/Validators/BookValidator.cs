using API.Requests;
using FluentValidation;

namespace API.Validators
{
    public class BookValidator : AbstractValidator<RequestBookModel>
    {
        public BookValidator()
        {
            RuleFor(exp => exp.Content).NotNull().NotEmpty().MinimumLength(10).WithMessage("Content field must not be empty or null and have at least 10 symbols");
            RuleFor(exp => exp.Title).NotNull().NotEmpty().MinimumLength(5).WithMessage("Title field must not be empty or null and have at least 5 symbols");
        }
    }
}
