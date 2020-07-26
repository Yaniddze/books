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
                .Must(x => x != Guid.Empty)
                .WithMessage("Bad guid");
            
            RuleFor(x => x.NewAuthorId)
                .NotNull()
                .Must(x => x != Guid.Empty)
                .WithMessage("Bad guid");;
            
            RuleFor(x => x.NewGenreId)
                .NotNull()
                .Must(x => x != Guid.Empty)
                .WithMessage("Bad guid");;
            
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