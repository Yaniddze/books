using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class FindBookTestQuery: IFindQuery<Book>
    {
        public Task<Book> FindOneAsync(Expression<Func<Book, bool>> pattern)
        {
            return Task.FromResult(Storage.Books.FirstOrDefault(pattern.Compile()));
        }

        public Task<IEnumerable<Book>> FindManyAsync(Expression<Func<Book, bool>> pattern)
        {
            return Task.FromResult(Storage.Books.Where(pattern.Compile()));
        }
    }
}