using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Installers;
using BooksApi.Installers.AutoMapper;
using BooksApi.UnitTests.TestCQRS;
using BooksApi.UnitTests.TestCQRS.AuthorImpl;
using BooksApi.UnitTests.TestCQRS.BookImpl;
using BooksApi.UnitTests.TestCQRS.GenreImpl;
using BooksApi.UseCases.UpdateBook;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BooksApi.UnitTests
{
    public class UpdateBookUseCaseTests
    {
        private readonly Storage _storage = new Storage();
        public UpdateBookUseCaseTests()
        {
            IServiceCollection services = new ServiceCollection();
            var autoMapperInstaller = new AutoMapperInstaller();
            
            autoMapperInstaller.InstallServices(services, null);
            
            _testable = new UpdateBookUseCase(
                new UpdateBookRequestValidator(), 
                new UpdateBookCommandTestHandler(_storage),
                services.BuildServiceProvider().GetService<IMapper>(),
                new FindAuthorTestQuery(_storage), 
                new FindGenreTestQuery(_storage),
                new FindBookTestQuery(_storage)
            );
        }

        private readonly UpdateBookUseCase _testable;

        [Fact]
        public async Task update_book_success()
        {
            var tempBook = _storage.Books[0];
            var tempAuthor = _storage.Authors[1];
            var tempGenre = _storage.Genres[0];
            var newTitle = "Updated title";
            var newYear = 898;
            
            var result = await _testable.Handle(new UpdateBookRequest
            {
                BookId = tempBook.Id.ToString(),
                NewAuthorId = tempAuthor.Id.ToString(),
                NewGenreId = tempGenre.Id.ToString(),
                NewTitle = newTitle,
                NewYear = newYear
            }, CancellationToken.None);
            
            Assert.True(result.Success);
            Assert.Equal(tempBook.BookInfo.Title, newTitle);
            Assert.Equal(tempBook.BookInfo.Year, newYear);
            Assert.Equal(tempBook.Author.Name, tempAuthor.Name);
            Assert.Equal(tempBook.Author.Id, tempAuthor.Id);
            Assert.Equal(tempBook.Genre.Id, tempGenre.Id);
            Assert.Equal(tempBook.Genre.Title, tempGenre.Title);
        } 
        
        [Theory]
        [InlineData(
            "24947815-f0b0-4027-9bd9-7ce39046191b", // Will be not found
            "e635b25f-a408-4db7-be15-3352b4d69425",
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7",
            "New title",
            228
        )]
        [InlineData(
            "8f2ae7c0-3399-4432-a79f-917ee18c15ea", 
            "e635b25f-a408-4db7-be15-3352b4d69422", // Will be not found
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7",
            "New title",
            228
        )]
        [InlineData(
            "8f2ae7c0-3399-4432-a79f-917ee18c15ea", 
            "e635b25f-a408-4db7-be15-3352b4d69425", 
            "dce3b0cf-e8c2-4430-af45-6e18df4596e2", // Will be not found
            "New title",
            228
        )]
        [InlineData(
            "8f2ae7c0-3399-4432-a79f-917ee18c15ea", 
            "e635b25f-a408-4db7-be15-3352b4d69425", 
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7",
            "New",
            228
        )]
        [InlineData(
            "8f2ae7c0-3399-4432-a79f-917ee18c15ea", 
            "e635b25f-a408-4db7-be15-3352b4d69425", 
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7",
            "New title",
            -1
        )]
        [InlineData(
            "8f2ae7c0-3399-4432-a79f-917ee18c15ea", 
            "e635b25f", 
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7",
            "New title",
            200
        )]
        [InlineData(
            "8f2ae7c0", 
            "e635b25f-a408-4db7-be15-3352b4d69425", 
            "dce3b0cf-e8c2-4430-af45-6e18df4596e7",
            "New title",
            200
        )]
        [InlineData(
            "8f2ae7c0-3399-4432-a79f-917ee18c15ea", 
            "e635b25f-a408-4db7-be15-3352b4d69425", 
            "dce3b0cf",
            "New title",
            200
        )]
        public async Task update_book_failed(
            string bookId,
            string newAuthorId,
            string newGenreId,
            string newTitle,
            int newYear
        )
        {
            var result = await _testable.Handle(new UpdateBookRequest
            {
                BookId = bookId,
                NewAuthorId = newAuthorId,
                NewGenreId = newGenreId,
                NewTitle = newTitle,
                NewYear = newYear
            }, CancellationToken.None);
            
            Assert.False(result.Success);
        } 
    }
}