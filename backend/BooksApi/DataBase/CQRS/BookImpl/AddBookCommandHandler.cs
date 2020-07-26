using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Commands;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;
using MediatR;

namespace BooksApi.DataBase.CQRS.BookImpl
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var mappedBook = _mapper.Map<Book, BookDB>(request.BookToAdd);

            mappedBook.AuthorId = request.BookToAdd.Author.Id;
            mappedBook.Author = null;
            mappedBook.GenreId = request.BookToAdd.Genre.Id;
            mappedBook.Genre = null;
            
            _context.Books.Add(mappedBook);
            
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}