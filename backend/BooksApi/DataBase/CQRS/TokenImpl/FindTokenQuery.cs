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

namespace BooksApi.DataBase.CQRS.TokenImpl
{
    public class FindTokenQuery: IFindQuery<Token>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public FindTokenQuery(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Token> FindOneAsync(Expression<Func<Token, bool>> pattern)
        {
            var mappedPattern = _mapper.Map<Expression<Func<Token, bool>>, Expression<Func<TokenDB, bool>>>(pattern);

            var founded = await _context.Tokens.FirstOrDefaultAsync(mappedPattern);

            return founded == null ? null : _mapper.Map<TokenDB, Token>(founded);
        }

        public async Task<IEnumerable<Token>> FindManyAsync(Expression<Func<Token, bool>> pattern)
        {
            var mappedPattern = _mapper.Map<Expression<Func<Token, bool>>, Expression<Func<TokenDB, bool>>>(pattern);

            var founded = await _context.Tokens
                .Where(mappedPattern)
                .Select(x => _mapper.Map<TokenDB, Token>(x))
                .ToListAsync();

            return founded;
        }
    }
}