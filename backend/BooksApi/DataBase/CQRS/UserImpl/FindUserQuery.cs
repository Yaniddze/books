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
        private readonly ContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public FindUserQuery(ContextProvider contextProvider, IMapper mapper)
        {
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task<User> FindOneAsync(Expression<Func<User, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedPattern = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);

                var founded = await context.Users.FirstOrDefaultAsync(mappedPattern);

                return founded == null ? null : _mapper.Map<UserDB, User>(founded);
            }
        }

        public async Task<IEnumerable<User>> FindManyAsync(Expression<Func<User, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedPattern = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);

                return await context.Users
                    .Where(mappedPattern)
                    .Select(x => _mapper.Map<UserDB, User>(x))
                    .ToListAsync();
            }
        }
    }
}