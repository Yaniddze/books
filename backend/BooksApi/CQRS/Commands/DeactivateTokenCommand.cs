using System;
using MediatR;

namespace BooksApi.CQRS.Commands
{
    public class DeactivateTokenCommand: IRequest
    {
        public Guid UserId { get; set; }
    }
}