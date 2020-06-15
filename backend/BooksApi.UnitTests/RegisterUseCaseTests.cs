using System.Threading;
using System.Threading.Tasks;
using BooksApi.UnitTests.TestCQRS;
using BooksApi.UnitTests.TestCQRS.UserImpl;
using BooksApi.UseCases.Register;
using Xunit;

namespace BooksApi.UnitTests
{
    public class RegisterUseCaseTests
    {
        private readonly RegisterUseCase _testable;

        public RegisterUseCaseTests()
        {
            var storage = new Storage();
            _testable = new RegisterUseCase(
                new AddUserCommandTestHandler(storage),
                new RegisterRequestValidator(), 
                new FindUserTestQuery(storage) 
            );
        }

        [Theory]
        [InlineData("somelogin", "password")]
        public async Task register_use_case_success(string login, string password)
        {
            var registerResult = await _testable.Handle(new RegisterRequest
            {
                Login = login,
                Password = password,
            }, CancellationToken.None);
            
            Assert.True(registerResult.Success);
        }
        
        [Theory]
        [InlineData("some login", "password")]
        [InlineData("some-login", "password")]
        [InlineData("some_login", "password")]
        [InlineData("som", "password")]
        [InlineData("someLogin", "pas")]
        public async Task register_use_case_failed(string login, string password)
        {
            var registerResult = await _testable.Handle(new RegisterRequest
            {
                Login = login,
                Password = password,
            }, CancellationToken.None);
            
            Assert.False(registerResult.Success);
        }
    }
}