using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Queries;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.DataBase.CQRS.BookImpl
{
    public class FindBookQuery: IFindQuery<Book>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public FindBookQuery(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Book> FindOneAsync(Expression<Func<Book, bool>> pattern)
        {
            var mappedPattern = _mapper.Map<Expression<Func<Book, bool>>, Expression<Func<BookDB, bool>>>(pattern);
            var founded = await _context.Books.FirstOrDefaultAsync(mappedPattern);
            return founded == null ? null : _mapper.Map<BookDB, Book>(founded);
        }

        public async Task<IEnumerable<Book>> FindManyAsync(Expression<Func<Book, bool>> pattern)
        {
            var mappedPattern = _mapper.Map<Expression<Func<Book, bool>>, Expression<Func<BookDB, bool>>>(pattern);
            return await _context.Books
                .Where(mappedPattern)
                .Select(x => _mapper.Map<BookDB, Book>(x))
                .ToListAsync();
        }
    }
}