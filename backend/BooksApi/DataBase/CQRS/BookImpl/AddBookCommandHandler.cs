using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;

namespace BooksApi.DataBase.CQRS.BookImpl
{
    public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task HandleAsync(AddBookCommand handled)
        {
            var mappedBook = _mapper.Map<Book, BookDB>(handled.BookToAdd);

            mappedBook.AuthorId = handled.BookToAdd.Author.Id;
            mappedBook.Author = null;
            mappedBook.GenreId = handled.BookToAdd.Genre.Id;
            mappedBook.Genre = null;
            
            await _context.Books.AddAsync(mappedBook);
        }
    }
}