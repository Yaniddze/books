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

namespace BooksApi.DataBase.CQRS.UserImpl
{
    public class FindUserQuery: IFindQuery<User>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public FindUserQuery(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<User> FindOneAsync(Expression<Func<User, bool>> pattern)
        {
            var mappedPattern = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);

            var founded = await _context.Users.FirstOrDefaultAsync(mappedPattern);

            return founded == null ? null : _mapper.Map<UserDB, User>(founded);
        }

        public async Task<IEnumerable<User>> FindManyAsync(Expression<Func<User, bool>> pattern)
        {
            var mappedPattern = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);

            return await _context.Users
                .Where(mappedPattern)
                .Select(x => _mapper.Map<UserDB, User>(x))
                .ToListAsync();
        }
    }
}