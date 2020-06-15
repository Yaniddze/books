using System.Threading;
using System.Threading.Tasks;
using BooksApi.UnitTests.TestCQRS;
using BooksApi.UnitTests.TestCQRS.UserImpl;
using BooksApi.UseCases.Login;
using Xunit;

namespace BooksApi.UnitTests
{
    public class LoginUseCaseTests
    {
        private readonly LoginUseCase _testable;

        public LoginUseCaseTests()
        {
            var storage = new Storage();
            _testable = new LoginUseCase(
                new FindUserTestQuery(storage),
                new LoginRequestValidator()
            );
        }

        [Theory]
        [InlineData("root", "root")]
        [InlineData("yaniddze", "somePass")]
        public async Task login_use_case_success(string login, string password)
        {
            var loginResult = await _testable.Handle(new LoginRequest
            {
                Login = login,
                Password = password,
            }, CancellationToken.None);
            
            Assert.True(loginResult.Success);
        }
        
        [Theory]
        [InlineData("root1", "root1")]
        [InlineData("yaniddze1", "somePass1")]
        public async Task login_use_case_failed(string login, string password)
        {
            var loginResult = await _testable.Handle(new LoginRequest
            {
                Login = login,
                Password = password,
            }, CancellationToken.None);
            
            Assert.False(loginResult.Success);
        }
    }
}