using System;
using FluentValidation;

namespace BooksApi.UseCases.AddBook
{
    public class AddBookRequestValidator: AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.Year)
                .NotNull()
                .Must(x => x >= 0 && x <= DateTime.Now.Year)
                .WithMessage("Year must more than 0 and less than actual year");
            RuleFor(x => x.AuthorId)
                .NotNull()
                .MinimumLength(36)
                .MaximumLength(36);
            RuleFor(x => x.GenreId)
                .NotNull()
                .MinimumLength(36)
                .MaximumLength(36);
            RuleFor(x => x.BookTitle)
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(50);
        }
    }
}