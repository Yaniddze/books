using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;

namespace BooksApi.UnitTests.TestCQRS.GenreImpl
{
    internal class FindGenreTestQuery: IFindQuery<Genre>
    {
        public Task<Genre> FindOneAsync(Expression<Func<Genre, bool>> pattern)
        {
            return Task.FromResult(Storage.Genres.FirstOrDefault(pattern.Compile()));
        }

        public Task<IEnumerable<Genre>> FindManyAsync(Expression<Func<Genre, bool>> pattern)
        {
            return Task.FromResult(Storage.Genres.Where(pattern.Compile()));
        }
    }
}