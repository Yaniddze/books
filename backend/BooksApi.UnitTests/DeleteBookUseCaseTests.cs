using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.UnitTests.TestCQRS;
using BooksApi.UnitTests.TestCQRS.BookImpl;
using BooksApi.UseCases.DeleteBooks;
using Xunit;

namespace BooksApi.UnitTests
{
    public class DeleteBookUseCaseTests
    {
        private readonly Storage _storage = new Storage();

        public DeleteBookUseCaseTests()
        {
            _testable = new DeleteBooksUseCase(
                new DeleteBookRequestValidator(), 
                new DeleteBookCommandTestHandler(_storage)
            );
        }

        private readonly DeleteBooksUseCase _testable;

        [Fact]
        public async Task delete_book_success()
        {
            var countsBefore = _storage.Books.Count;
            
            var result = await _testable.Handle(new DeleteBooksRequest
            {
                BookIds = new Guid[]
                {
                    _storage.Books[0].Id, 
                    _storage.Books[1].Id
                },
            }, CancellationToken.None);
            
            Assert.Equal(countsBefore - 2, _storage.Books.Count);
        }
        
        [Theory]
        [InlineData(new object[]
        {
            new string[] {  }, 
        })]
        public async Task delete_book_failed(string[] ids)
        {
            var countsBefore = _storage.Books.Count;
            
            var result = await _testable.Handle(new DeleteBooksRequest
            {
                BookIds = ids.Select(Guid.Parse).ToArray(),
            }, CancellationToken.None);
            
            Assert.False(result.Success);
        }
    }
}
