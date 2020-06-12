using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class AddBookCommandTestHandler: ICommandHandler<AddBookCommand>
    {
        private readonly Storage _storage;

        public AddBookCommandTestHandler(Storage storage)
        {
            _storage = storage;
        }
        public async Task HandleAsync(AddBookCommand handled)
        {
            _storage.Books.Add(handled.BookToAdd);
        }
    }
}