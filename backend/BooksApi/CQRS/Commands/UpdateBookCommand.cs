using System;
using MediatR;

namespace BooksApi.CQRS.Commands
{
    public class UpdateBookCommand: IRequest
    {
        public Guid BookId { get; set; }
        public int NewYear { get; set; }
        public string NewTitle { get; set; }
        public Guid NewAuthorId { get; set; }
        public Guid NewGenreId { get; set; }
    }
}