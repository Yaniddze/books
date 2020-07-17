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
        private readonly IContext _context;

        public DeleteBookCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(DeleteBooksCommand handled)
        {
            await _context.Books
                .Where(book => handled.BookIds
                    .Any(id => book.Id == id))
                .DeleteAsync();
        }
    }
}