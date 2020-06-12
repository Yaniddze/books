using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class DeleteBookCommandTestHandler: ICommandHandler<DeleteBooksCommand>
    {
        public async Task HandleAsync(DeleteBooksCommand handled)
        {
            Storage.Books.RemoveAll(book => handled.BookIds.Contains(book.Id));
        }
    }
}