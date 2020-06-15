using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.Entities;

namespace BooksApi.CQRS.Commands
{
    public class AddUserCommand: ICommand
    {
        public User UserToAdd { get; set; }
    }
}