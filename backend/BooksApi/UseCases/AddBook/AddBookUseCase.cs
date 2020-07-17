using System;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.AddBook
{
    public class AddBookUseCase : AbstractUseCase<AddBookRequest, Guid>
    {
        private readonly ICommandHandler<AddBookCommand> _handler;
        private readonly IFindQuery<Author> _authorFinder;
        private readonly IFindQuery<Genre> _genreFinder;

        public AddBookUseCase(
            ICommandHandler<AddBookCommand> handler, 
            IValidator<AddBookRequest> validator,
            IFindQuery<Author> authorFinder, 
            IFindQuery<Genre> genreFinder
        ): base(validator)
        {
            _handler = handler;
            _authorFinder = authorFinder;
            _genreFinder = genreFinder;
        }

        protected override async Task<AbstractAnswer<Guid>> HandleAsync(AddBookRequest request, CancellationToken cancellationToken)
        {
            var foundedAuthor = await _authorFinder.FindOneAsync(x => x.Id == Guid.Parse(request.AuthorId));
            if (foundedAuthor == null)
            {
                return CreateBadAnswer(new[] { "Author not found" });
            }

            var foundedGenre = await _genreFinder.FindOneAsync(x => x.Id == Guid.Parse(request.GenreId));
            if (foundedGenre == null)
            {
                return CreateBadAnswer(new[] { "Genre not found" });
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

            return CreateSuccessAnswer(tempBook.Id);
        }
    }
}