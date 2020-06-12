using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class AddBookCommandTestHandler: ICommandHandler<AddBookCommand>
    {
        public async Task HandleAsync(AddBookCommand handled)
        {
            Storage.Books.Add(handled.BookToAdd);
        }
    }
}