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
        private readonly ContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public FindGenreQuery(IMapper mapper, ContextProvider contextProvider)
        {
            _mapper = mapper;
            _contextProvider = contextProvider;
        }
        public async Task<Genre> FindOneAsync(Expression<Func<Genre, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedPattern = 
                    _mapper.Map<Expression<Func<Genre, bool>>, Expression<Func<GenreDB, bool>>>(pattern);
                var founded = await context.Genres.FirstOrDefaultAsync(mappedPattern);
                return founded == null ? null : _mapper.Map<GenreDB, Genre>(founded);
            }
        }

        public async Task<IEnumerable<Genre>> FindManyAsync(Expression<Func<Genre, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedPattern = 
                    _mapper.Map<Expression<Func<Genre, bool>>, Expression<Func<GenreDB, bool>>>(pattern);
                return await context.Genres
                    .Where(mappedPattern)
                    .Select(x => _mapper.Map<GenreDB, Genre>(x))
                    .ToListAsync();
            }
        }
    }
}