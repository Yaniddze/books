using System;
using BooksApi.CQRS.Commands.Abstractions;

namespace BooksApi.CQRS.Commands
{
    public class UpdateBookCommand: ICommand
    {
        public Guid BookId { get; set; }
        public int NewYear { get; set; }
        public string NewTitle { get; set; }
        public Guid NewAuthorId { get; set; }
        public Guid NewGenreId { get; set; }
    }
}