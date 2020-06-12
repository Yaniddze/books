using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class UpdateBookCommandTestHandler: ICommandHandler<UpdateBookCommand>
    {
        private readonly Storage _storage;

        public UpdateBookCommandTestHandler(Storage storage)
        {
            _storage = storage;
        }
        public async Task HandleAsync(UpdateBookCommand handled)
        {
            var founded = _storage.Books.First(x => x.Id == handled.BookId);
            founded.Author = _storage.Authors.First(x => x.Id == handled.NewAuthorId);
            founded.Genre = _storage.Genres.First(x => x.Id == handled.NewGenreId);
            founded.BookInfo.Title = handled.NewTitle;
            founded.BookInfo.Year = handled.NewYear;
        }
    }
}