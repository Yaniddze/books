using System;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.Register
{
    public class RegisterUseCase : AbstractUseCase<RegisterRequest, User>
    {
        private readonly IFindQuery<User> _findQuery;

        public RegisterUseCase(
            IValidator<RegisterRequest> validator,
            IFindQuery<User> findQuery)
        : base(validator)
        {
            _findQuery = findQuery;
        }

        protected override async Task<AbstractAnswer<User>> HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var foundedUser = await _findQuery.FindOneAsync(x => x.LoginInfo.Login == request.Login);
            if (foundedUser != null)
            {
                return CreateBadAnswer(new[] {"User with this login is already exists"});
            }

            var userToAdd = new User
            {
                Id = Guid.NewGuid(),
                LoginInfo = new LoginInfo
                {
                    Login = request.Login,
                    Password = request.Password,
                }
            };

            return CreateSuccessAnswer(userToAdd);
        }
    }
}