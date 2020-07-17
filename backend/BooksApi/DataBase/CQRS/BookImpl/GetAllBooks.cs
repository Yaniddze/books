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
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetAllBooks(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Book>> InvokeAsync()
        {
            return await _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Select(x => _mapper.Map<BookDB, Book>(x))
                .ToListAsync();
        }
    }
}