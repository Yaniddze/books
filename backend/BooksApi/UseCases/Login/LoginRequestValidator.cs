using FluentValidation;

namespace BooksApi.UseCases.Login
{
    public class LoginRequestValidator: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
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