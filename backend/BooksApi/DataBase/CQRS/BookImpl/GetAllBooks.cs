using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Queries;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.DataBase.CQRS.BookImpl
{
    public class GetAllBooks: IGetAllQuery<Book>
    {
        private readonly ContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public GetAllBooks(ContextProvider contextProvider, IMapper mapper)
        {
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Book>> InvokeAsync()
        {
            using (var context = _contextProvider.GetContext())
            {
                return await context.Books
                    .Include(x => x.Author)
                    .Include(x => x.Genre)
                    .Select(x => _mapper.Map<BookDB, Book>(x))
                    .ToListAsync();
            }
        }
    }
}