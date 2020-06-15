using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using BooksApi.UseCases.AddBook;
using BooksApi.UseCases.DeleteBooks;
using BooksApi.UseCases.Login;
using BooksApi.UseCases.Register;
using BooksApi.UseCases.UpdateBook;

namespace BooksApi.UnitTests
{
    public class ValidatorsTests
    {
        // Add book request Success
        [Theory]
        [InlineData(
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "Some title",
            200
        )]
        [InlineData(
            "fadde3dd-412d-4d9b-b1cf-ab86a409d155",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "Some title",
            10
        )]
        public async Task add_book_validation_success(
            string authorId,
            string genreId,
            string title,
            int year
        )
        {
            var request = new AddBookRequest
            {
                AuthorId = authorId,
                BookTitle = title,
                GenreId = genreId,
                Year = year,
            };

            var validator = new AddBookRequestValidator();

            var validationResult = await validator.ValidateAsync(request);

            Assert.True(validationResult.IsValid);
        }

        // Add book request failed
        [Theory]
        [InlineData(
            "8e60cdec",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "Some title",
            200
        )]
        [InlineData(
            "fadde3dd-412d-4d9b-b1cf-ab86a409d155",
            "8e60cdec",
            "Some title",
            10
        )]
        [InlineData(
            "fadde3dd-412d-4d9b-b1cf-ab86a409d155",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "Some",
            10
        )]
        [InlineData(
            "fadde3dd-412d-4d9b-b1cf-ab86a409d155",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "Some title",
            -1
        )]
        [InlineData(
            "fadde3dd-412d-4d9b-b1cf-ab86a409d155",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "Some title",
            2050
        )]
        public async Task add_book_validation_failed(
            string authorId,
            string genreId,
            string title,
            int year
        )
        {
            var request = new AddBookRequest
            {
                AuthorId = authorId,
                BookTitle = title,
                GenreId = genreId,
                Year = year,
            };

            var validator = new AddBookRequestValidator();

            var validationResult = await validator.ValidateAsync(request);

            Assert.False(validationResult.IsValid);
        }

        // Delete book request failed
        [Theory]
        [InlineData(new object[] { new string[] { } })]
        public async Task delete_book_validation_failed(string[] ids)
        {
            var guids = ids.Select(Guid.Parse);
            var request = new DeleteBooksRequest
            {
                BookIds = guids.ToArray(),
            };

            var validator = new DeleteBookRequestValidator();

            var validationResult = await validator.ValidateAsync(request);

            Assert.False(validationResult.IsValid);
        }

        // Delete book request success
        [Theory]
        [InlineData(new object[]
        {
            new string[]
            {
                "fadde3dd-412d-4d9b-b1cf-ab86a409d155",
                "3cc42b4e-78ad-4cf2-9695-99af6dd03009"
            }
        })]
        public async Task delete_book_validation_success(
            string[] ids
        )
        {
            var guids = ids.Select(Guid.Parse);
            var request = new DeleteBooksRequest
            {
                BookIds = guids.ToArray(),
            };

            var validator = new DeleteBookRequestValidator();

            var validationResult = await validator.ValidateAsync(request);

            Assert.True(validationResult.IsValid);
        }
        
        // Update book request success
        [Theory]
        [InlineData(
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            123,
            "Some new title",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0"
        )]
        [InlineData(
            "3cc42b4e-78ad-4cf2-9695-99af6dd03009",
            2020,
            "Some new title 2",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "41faf1b4-b28c-4985-b8f2-fa6b798bcdd1"
        )]
        public async Task update_book_validation_success(
            string bookId,
            int newYear,
            string newTitle,
            string newAuthorId,
            string newGenereId
        )
        {
            var request = new UpdateBookRequest
            {
                BookId = bookId,
                NewTitle = newTitle,
                NewAuthorId = newAuthorId,
                NewGenreId = newGenereId,
                NewYear = newYear
            };
            
            var validator = new UpdateBookRequestValidator();

            var validationResult = await validator.ValidateAsync(request);
            
            Assert.True(validationResult.IsValid);
        }
        
        // Update book request failed
        [Theory]
        [InlineData(
            "8e60cdec-8446",
            123,
            "Some new title",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0"
        )]
        [InlineData(
            "3cc42b4e-78ad-4cf2-9695-99af6dd03009",
            -1,
            "Some new title",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0"
        )]
        [InlineData(
            "3cc42b4e-78ad-4cf2-9695-99af6dd03009",
            120,
            "Some",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0"
        )]
        [InlineData(
            "3cc42b4e-78ad-4cf2-9695-99af6dd03009",
            120,
            "Some new Title",
            "8e60cdec-8446",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0"
        )]
        [InlineData(
            "3cc42b4e-78ad-4cf2-9695-99af6dd03009",
            120,
            "Some new Title",
            "8e60cdec-8446-4a06-b411-c1dfacb160c0",
            "8e60cdec-8446"
        )]
        public async Task update_book_validation_failed(
            string bookId,
            int newYear,
            string newTitle,
            string newAuthorId,
            string newGenereId
        )
        {
            var request = new UpdateBookRequest
            {
                BookId = bookId,
                NewTitle = newTitle,
                NewAuthorId = newAuthorId,
                NewGenreId = newGenereId,
                NewYear = newYear
            };
            
            var validator = new UpdateBookRequestValidator();

            var validationResult = await validator.ValidateAsync(request);
            
            Assert.False(validationResult.IsValid);
        }
        
        // Validate login request success
        [Theory]
        [InlineData("someLogin", "somePass")]
        public async Task login_validate_success(string login, string password)
        {
            var request = new LoginRequest {Login = login, Password = password};
            
            var validator = new LoginRequestValidator();

            var validationResult = await validator.ValidateAsync(request, CancellationToken.None);

            Assert.True(validationResult.IsValid);
        }
        
        // Validate login request failed
        [Theory]
        [InlineData("s", "somePass")]
        [InlineData("someLogin", "s")]
        public async Task login_validate_failed(string login, string password)
        {
            var request = new LoginRequest {Login = login, Password = password};
            
            var validator = new LoginRequestValidator();

            var validationResult = await validator.ValidateAsync(request, CancellationToken.None);

            Assert.False(validationResult.IsValid);
        }
        
        // Validate register request success
        [Theory]
        [InlineData("ssssss", "somePass")]
        [InlineData("someLogin", "ssssss")]
        public async Task register_validate_success(string login, string password)
        {
            var request = new RegisterRequest {Login = login, Password = password};
            
            var validator = new RegisterRequestValidator();

            var validationResult = await validator.ValidateAsync(request, CancellationToken.None);

            Assert.True(validationResult.IsValid);
        }
        
        // Validate register request failed
        [Theory]
        [InlineData("s", "somePass")]
        [InlineData("someLogin", "s")]
        [InlineData("some-login", "ssssss")]
        [InlineData("some login", "ssssss")]
        [InlineData("some_login", "ssssss")]
        public async Task register_validate_failed(string login, string password)
        {
            var request = new RegisterRequest {Login = login, Password = password};
            
            var validator = new RegisterRequestValidator();

            var validationResult = await validator.ValidateAsync(request, CancellationToken.None);

            Assert.False(validationResult.IsValid);
        }
    }
}