using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class UpdateBookCommandTestHandler: ICommandHandler<UpdateBookCommand>
    {
        public async Task HandleAsync(UpdateBookCommand handled)
        {
            var founded = Storage.Books.First(x => x.Id == handled.BookId);
            founded.Author = Storage.Authors.First(x => x.Id == handled.NewAuthorId);
            founded.Genre = Storage.Genres.First(x => x.Id == handled.NewGenreId);
            founded.BookInfo.Title = handled.NewTitle;
            founded.BookInfo.Year = handled.NewYear;
        }
    }
}