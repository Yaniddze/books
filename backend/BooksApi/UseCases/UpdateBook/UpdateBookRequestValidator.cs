using System;
using FluentValidation;

namespace BooksApi.UseCases.UpdateBook
{
    public class UpdateBookRequestValidator: AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(x => x.BookId)
                .NotNull()
                .MinimumLength(36)
                .MaximumLength(36);
            
            RuleFor(x => x.NewAuthorId)
                .NotNull()
                .MinimumLength(36)
                .MaximumLength(36);
            
            RuleFor(x => x.NewGenreId)
                .NotNull()
                .MinimumLength(36)
                .MaximumLength(36);
            
            RuleFor(x => x.NewTitle)
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(x => x.NewYear)
                .Must(x => x >= 0 && x <= DateTime.Now.Year)
                .WithMessage("Year must be between 0 and actual year");
        }
    }
}