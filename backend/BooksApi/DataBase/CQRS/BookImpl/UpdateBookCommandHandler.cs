using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using Z.EntityFramework.Plus;

namespace BooksApi.DataBase.CQRS.BookImpl
{
    public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand>
    {
        private readonly IContext _context;

        public UpdateBookCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(UpdateBookCommand handled)
        {
            await _context.Books
                .Where(x => x.Id == handled.BookId)
                .UpdateAsync(x => new BookDB
                {
                    Title = handled.NewTitle,
                    Year = handled.NewYear,
                    AuthorId = handled.NewAuthorId,
                    GenreId = handled.NewGenreId,
                });
        }
    }
}