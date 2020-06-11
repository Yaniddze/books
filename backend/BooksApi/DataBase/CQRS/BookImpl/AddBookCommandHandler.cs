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
        private readonly ContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(ContextProvider contextProvider, IMapper mapper)
        {
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task HandleAsync(AddBookCommand handled)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedBook = _mapper.Map<Book, BookDB>(handled.BookToAdd);

                mappedBook.AuthorId = handled.BookToAdd.Author.Id;
                mappedBook.Author = null;
                mappedBook.GenreId = handled.BookToAdd.Genre.Id;
                mappedBook.Genre = null;
                
                context.Books.Add(mappedBook);

                await context.SaveChangesAsync();
            }
        }
    }
}