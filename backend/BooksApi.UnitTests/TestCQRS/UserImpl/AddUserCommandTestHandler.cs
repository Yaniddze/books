using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.UnitTests.TestCQRS.UserImpl
{
    internal class AddUserCommandTestHandler: ICommandHandler<AddUserCommand>
    {
        private readonly Storage _storage;

        public AddUserCommandTestHandler(Storage storage)
        {
            _storage = storage;
        }

        public Task HandleAsync(AddUserCommand handled)
        {
            _storage.Users.Add(handled.UserToAdd);
            return Task.CompletedTask;
        }
    }
}