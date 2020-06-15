using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;

namespace BooksApi.UnitTests.TestCQRS.UserImpl
{
    internal class FindUserTestQuery: IFindQuery<User>
    {
        private readonly Storage _storage;

        public FindUserTestQuery(Storage storage)
        {
            _storage = storage;
        }

        public Task<User> FindOneAsync(Expression<Func<User, bool>> pattern)
        {
            return Task.FromResult(_storage.Users.FirstOrDefault(pattern.Compile()));
        }

        public Task<IEnumerable<User>> FindManyAsync(Expression<Func<User, bool>> pattern)
        {
            return Task.FromResult(_storage.Users.Where(pattern.Compile()));
        }
    }
}