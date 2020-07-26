using System;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.UpdateBook
{
    public class UpdateBookRequest: IRequest<AbstractAnswer>
    {
        public Guid BookId { get; set; }
        public int NewYear { get; set; }
        public string NewTitle { get; set; }
        public Guid NewAuthorId { get; set; }
        public Guid NewGenreId { get; set; }
    }
}