using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.DataBase.Context;
using Z.EntityFramework.Plus;

namespace BooksApi.DataBase.CQRS.BookImpl
{
    public class DeleteBookCommandHandler: ICommandHandler<DeleteBooksCommand>
    {
        private readonly ContextProvider _contextProvider;

        public DeleteBookCommandHandler(ContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public async Task HandleAsync(DeleteBooksCommand handled)
        {
            using (var context = _contextProvider.GetContext())
            {
                await context.Books
                    .Where(book => handled.BookIds
                        .Any(id => book.Id == id))
                    .DeleteAsync();
            }
        }
    }
}