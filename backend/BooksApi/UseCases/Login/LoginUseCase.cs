using System;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.Login
{
    public class LoginUseCase: AbstractUseCase<LoginRequest, Guid>
    {
        private readonly IFindQuery<User> _findUser;

        public LoginUseCase(IFindQuery<User> findUser, IValidator<LoginRequest> validator)
        : base(validator)
        {
            _findUser = findUser;
        }

        protected override async Task<AbstractAnswer<Guid>> HandleAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var foundedUser = await _findUser.FindOneAsync(x =>
                x.LoginInfo.Login == request.Login && x.LoginInfo.Password == request.Password);

            if (foundedUser == null)
            {
                return CreateBadAnswer(new[] {"User not found."});
            }

            return CreateSuccessAnswer(foundedUser.Id);
        }
    }
}