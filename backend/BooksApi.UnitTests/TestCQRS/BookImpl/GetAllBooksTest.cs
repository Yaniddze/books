using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class GetAllBooksTest: IGetAllQuery<Book>
    {
        private readonly Storage _storage;

        public GetAllBooksTest(Storage storage)
        {
            _storage = storage;
        }
        public Task<IEnumerable<Book>> InvokeAsync()
        {
            return Task.FromResult(_storage.Books.Select(x => x));
        }
    }
}