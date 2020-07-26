using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.DataBase.Context;
using MediatR;
using Z.EntityFramework.Plus;

namespace BooksApi.DataBase.CQRS.BookImpl
{
    public class DeleteBookCommandHandler: IRequestHandler<DeleteBooksCommand>
    {
        private readonly IContext _context;

        public DeleteBookCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBooksCommand request, CancellationToken cancellationToken)
        {
            await _context.Books
                .Where(book => request.BookIds
                    .Any(id => book.Id == id))
                .DeleteAsync(cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}