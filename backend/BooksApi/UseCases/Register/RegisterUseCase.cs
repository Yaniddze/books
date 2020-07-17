using System;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.Register
{
    public class RegisterUseCase : AbstractUseCase<RegisterRequest, Guid>
    {
        private readonly ICommandHandler<AddUserCommand> _commandHandler;
        private readonly IFindQuery<User> _findQuery;

        public RegisterUseCase(
            ICommandHandler<AddUserCommand> commandHandler, 
            IValidator<RegisterRequest> validator,
            IFindQuery<User> findQuery)
        : base(validator)
        {
            _commandHandler = commandHandler;
            _findQuery = findQuery;
        }

        protected override async Task<AbstractAnswer<Guid>> HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
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

            await _commandHandler.HandleAsync(new AddUserCommand {UserToAdd = userToAdd});

            return CreateSuccessAnswer(userToAdd.Id);
        }
    }
}