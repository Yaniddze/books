using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;

namespace BooksApi.UnitTests.TestCQRS.BookImpl
{
    internal class GetAllBooksTest: IGetAllQuery<Book>
    {
        public Task<IEnumerable<Book>> InvokeAsync()
        {
            return Task.FromResult(Storage.Books.Select(x => x));
        }
    }
}