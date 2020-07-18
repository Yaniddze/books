using FluentValidation;

namespace BooksApi.UseCases.RefreshToken
{
    public class RefreshTokenRequestValidator: AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshTokenId)
                .NotNull();
        }
    }
}