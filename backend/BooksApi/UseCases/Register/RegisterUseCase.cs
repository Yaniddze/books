using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using FluentValidation;
using MediatR;

namespace BooksApi.UseCases.Register
{
    public class RegisterUseCase : IRequestHandler<RegisterRequest, RegisterAnswer>
    {
        private readonly ICommandHandler<AddUserCommand> _commandHandler;
        private readonly IFindQuery<User> _findQuery;
        private readonly IValidator<RegisterRequest> _validator;

        public RegisterUseCase(
            ICommandHandler<AddUserCommand> commandHandler, 
            IValidator<RegisterRequest> validator,
            IFindQuery<User> findQuery)
        {
            _commandHandler = commandHandler;
            _validator = validator;
            _findQuery = findQuery;
        }

        public async Task<RegisterAnswer> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new RegisterAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage),
                };
            }

            var foundedUser = await _findQuery.FindOneAsync(x => x.LoginInfo.Login == request.Login);
            if (foundedUser != null)
            {
                return new RegisterAnswer
                {
                    Success = false,
                    Errors = new []{ "User with this login is already exists" },
                };
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

            return new RegisterAnswer
            {
                Success = true,
                UserId = userToAdd.Id,
            };
        }
    }
}