using BooksApi.Entities;
using MediatR;

namespace BooksApi.CQRS.Commands
{
    public class AddUserCommand: IRequest
    {
        public User UserToAdd { get; set; }
    }
}