using FluentValidation;

namespace BooksApi.UseCases.Register
{
    public class RegisterRequestValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotNull()
                .MinimumLength(4)
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(4)
                .MaximumLength(30);
        }
    }
}