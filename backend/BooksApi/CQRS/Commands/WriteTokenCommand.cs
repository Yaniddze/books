using System;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.CQRS.Commands
{
    public class WriteTokenCommand: ICommand
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string JwtId { get; set; }
    }
}