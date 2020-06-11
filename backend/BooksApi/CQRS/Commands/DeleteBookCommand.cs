using System;

namespace BooksApi.CQRS.Commands
{
    public class DeleteBookCommand
    {
        public Guid[] BookId { get; set; }
    }
}