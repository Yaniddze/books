using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using FluentValidation;
using MediatR;

namespace BooksApi.UseCases.AddBook
{
    public class AddBookUseCase : IRequestHandler<AddBookRequest, AddBookAnswer>
    {
        private readonly ICommandHandler<AddBookCommand> _handler;
        private readonly IFindQuery<Author> _authorFinder;
        private readonly IFindQuery<Genre> _genreFinder;
        private readonly IValidator<AddBookRequest> _validator;

        public AddBookUseCase(
            ICommandHandler<AddBookCommand> handler, 
            IValidator<AddBookRequest> validator,
            IFindQuery<Author> authorFinder, 
            IFindQuery<Genre> genreFinder
        )
        {
            _handler = handler;
            _validator = validator;
            _authorFinder = authorFinder;
            _genreFinder = genreFinder;
        }

        public async Task<AddBookAnswer> Handle(AddBookRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new AddBookAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage)
                };
            }

            var foundedAuthor = await _authorFinder.FindOneAsync(x => x.Id == Guid.Parse(request.AuthorId));
            if (foundedAuthor == null)
            {
                return new AddBookAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "Author not found" }
                };
            }

            var foundedGenre = await _genreFinder.FindOneAsync(x => x.Id == Guid.Parse(request.GenreId));
            if (foundedGenre == null)
            {
                return new AddBookAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "Genre not found" }
                };
            }

            var tempBook = new Book
            {
                Id = Guid.NewGuid(),
                Author = foundedAuthor,
                Genre = foundedGenre,
                BookInfo = new BookInfo
                {
                    Title = request.BookTitle,
                    Year = request.Year,
                },
            };

            await _handler.HandleAsync(new AddBookCommand{BookToAdd = tempBook});

            return new AddBookAnswer
            {
                Success = true,
                BookId = tempBook.Id,
            };
        }
    }
}