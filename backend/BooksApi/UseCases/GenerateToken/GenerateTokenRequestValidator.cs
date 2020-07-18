using FluentValidation;

namespace BooksApi.UseCases.GenerateToken
{
    public class GenerateTokenRequestValidator: AbstractValidator<GenerateTokenRequest>
    {
        public GenerateTokenRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull();
        }
    }
}