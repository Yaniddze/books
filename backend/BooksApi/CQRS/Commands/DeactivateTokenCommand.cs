using System;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.CQRS.Commands
{
    public class DeactivateTokenCommand: ICommand
    {
        public Guid UserId { get; set; }
    }
}