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

namespace BooksApi.DataBase.CQRS.GenreImpl
{
    public class FindGenreQuery: IFindQuery<Genre>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public FindGenreQuery(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Genre> FindOneAsync(Expression<Func<Genre, bool>> pattern)
        {
            var mappedPattern = 
                _mapper.Map<Expression<Func<Genre, bool>>, Expression<Func<GenreDB, bool>>>(pattern);
            var founded = await _context.Genres.FirstOrDefaultAsync(mappedPattern);
            return founded == null ? null : _mapper.Map<GenreDB, Genre>(founded);
        }

        public async Task<IEnumerable<Genre>> FindManyAsync(Expression<Func<Genre, bool>> pattern)
        {
            var mappedPattern = 
                _mapper.Map<Expression<Func<Genre, bool>>, Expression<Func<GenreDB, bool>>>(pattern);
            return await _context.Genres
                .Where(mappedPattern)
                .Select(x => _mapper.Map<GenreDB, Genre>(x))
                .ToListAsync();
        }
    }
}