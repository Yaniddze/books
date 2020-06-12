using System.Threading;
using System.Threading.Tasks;
using BooksApi.UnitTests.TestCQRS;
using BooksApi.UnitTests.TestCQRS.AuthorImpl;
using BooksApi.UnitTests.TestCQRS.BookImpl;
using BooksApi.UnitTests.TestCQRS.GenreImpl;
using BooksApi.UseCases.AddBook;
using Xunit;

namespace BooksApi.UnitTests
{
    public class AddBookUseCaseTests
    {
        private readonly AddBookUseCase _testable;
        private readonly Storage _storage = new Storage();

        public AddBookUseCaseTests()
        {
            _testable = new AddBookUseCase(
                new AddBookCommandTestHandler(_storage), 
                new AddBookRequestValidator(),
                new FindAuthorTestQuery(_storage), 
                new FindGenreTestQuery(_storage)
            );
        }
        
        [Theory]
        [InlineData(
            "new book title",
            220,
            "e635b25f-a408-4db7-be15-3352b4d69425",
            "ef1148ea-fc3a-46b5-850e-438d1107e257"
        )]
        [InlineData(
            "new book title 2",
            20,
            "ef1148ea-fc3a-46b5-850e-438d1107e257",
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7"
        )]
        public async Task add_book_success(
            string bookTitle,
            int year,
            string authorId,
            string genreId
        )
        {
            var result = await _testable.Handle(new AddBookRequest
            {
                BookTitle = bookTitle,
                Year = year,
                AuthorId = authorId,
                GenreId = genreId,
            }, CancellationToken.None);
            
            Assert.True(result.Success);
        }
        
        [Theory]
        [InlineData(
            "ne",
            220,
            "e635b25f-a408-4db7-be15-3352b4d69425",
            "ef1148ea-fc3a-46b5-850e-438d1107e257"
        )]
        [InlineData(
            "new book title 2",
            -20,
            "ef1148ea-fc3a-46b5-850e-438d1107e257",
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7"
        )]
        [InlineData(
            "new book title 2",
            20,
            "ef1148ea-fc3a-46b5-850e-438d1107e224", // Author with this guid doesn't exist
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7"
        )]
        [InlineData(
            "new book title 2",
            20,
            "ef1148ea-fc3a-46b5-850e-438d1107e224", 
            "dce3b0cf-e8c2-4430-af45-6e18df4596e4"  // Genre with this guid doesn't exist
        )]
        [InlineData(
            "new book title 2",
            20,
            "ef1148ea", 
            "dce3b0cf-e8c2-4430-af45-6e18df4596e4"
        )]
        [InlineData(
            "new book title 2",
            20,
            "ef1148ea-fc3a-46b5-850e-438d1107e224", 
            "dce3b0cf"
        )]
        public async Task add_book_failed(
            string bookTitle,
            int year,
            string authorId,
            string genreId
        )
        {
            var booksCountBefore = _storage.Books.Count;
            var result = await _testable.Handle(new AddBookRequest
            {
                BookTitle = bookTitle,
                Year = year,
                AuthorId = authorId,
                GenreId = genreId,
            }, CancellationToken.None);
            
            Assert.False(result.Success);
            Assert.Equal(booksCountBefore, _storage.Books.Count);
        }
    }
}