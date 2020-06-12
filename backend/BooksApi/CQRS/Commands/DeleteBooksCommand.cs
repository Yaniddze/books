using System;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.CQRS.Commands
{
    public class DeleteBooksCommand: ICommand
    {
        public Guid[] BookIds { get; set; }
    }
}