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

namespace BooksApi.DataBase.CQRS.AuthorImpl
{
    public class FindAuthorQuery: IFindQuery<Author>
    {
        private readonly ContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public FindAuthorQuery(IMapper mapper, ContextProvider contextProvider)
        {
            _mapper = mapper;
            _contextProvider = contextProvider;
        }

        public async Task<Author> FindOneAsync(Expression<Func<Author, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedPattern = 
                    _mapper.Map<Expression<Func<Author, bool>>, Expression<Func<AuthorDB, bool>>>(pattern);
                var founded = await context.Authors.FirstOrDefaultAsync(mappedPattern);
                return founded == null ? null : _mapper.Map<AuthorDB, Author>(founded);
            }
        }

        public async Task<IEnumerable<Author>> FindManyAsync(Expression<Func<Author, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedPattern = 
                    _mapper.Map<Expression<Func<Author, bool>>, Expression<Func<AuthorDB, bool>>>(pattern);

                return await context.Authors
                    .Where(mappedPattern)
                    .Select(x => _mapper.Map<AuthorDB, Author>(x))
                    .ToListAsync();
            }
            
        }
    }
}