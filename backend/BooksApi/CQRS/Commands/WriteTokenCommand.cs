using System;
using MediatR;

namespace BooksApi.CQRS.Commands
{
    public class WriteTokenCommand: IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}