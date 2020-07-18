using System;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.CQRS.Commands
{
    public class WriteTokenCommand: ICommand
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}