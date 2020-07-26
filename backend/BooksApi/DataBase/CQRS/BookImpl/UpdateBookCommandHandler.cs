using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using MediatR;
using Z.EntityFramework.Plus;

namespace BooksApi.DataBase.CQRS.BookImpl
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IContext _context;

        public UpdateBookCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            await _context.Books
                .Where(x => x.Id == request.BookId)
                .UpdateAsync(x => new BookDB
                {
                    Title = request.NewTitle,
                    Year = request.NewYear,
                    AuthorId = request.NewAuthorId,
                    GenreId = request.NewGenreId,
                }, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}