using FluentValidation;

namespace BooksApi.UseCases.DeleteBooks
{
    public class DeleteBookRequestValidator: AbstractValidator<DeleteBooksRequest>
    {
        public DeleteBookRequestValidator()
        {
            RuleFor(x => x.BookIds)
                .NotNull()
                .Must(x => x.Length > 0)
                .WithMessage("You must add id");
        }
    }
}