using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;

namespace BooksApi.UnitTests.TestCQRS.AuthorImpl
{
    internal class FindAuthorTestQuery: IFindQuery<Author>
    {
        public Task<Author> FindOneAsync(Expression<Func<Author, bool>> pattern)
        {
            return Task.FromResult(Storage.Authors.FirstOrDefault(pattern.Compile()));
        }

        public Task<IEnumerable<Author>> FindManyAsync(Expression<Func<Author, bool>> pattern)
        {
            return Task.FromResult(Storage.Authors.Where(pattern.Compile()));
        }
    }
}