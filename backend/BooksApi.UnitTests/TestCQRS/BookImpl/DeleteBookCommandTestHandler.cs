using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class DeleteBookCommandTestHandler: ICommandHandler<DeleteBooksCommand>
    {
        private readonly Storage _storage;

        public DeleteBookCommandTestHandler(Storage storage)
        {
            _storage = storage;
        }
        public async Task HandleAsync(DeleteBooksCommand handled)
        {
            _storage.Books.RemoveAll(book => handled.BookIds.Contains(book.Id));
        }
    }
}