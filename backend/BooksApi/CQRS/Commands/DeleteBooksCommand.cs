using System;
using MediatR;

namespace BooksApi.CQRS.Commands
{
    public class DeleteBooksCommand: IRequest
    {
        public Guid[] BookIds { get; set; }
    }
}