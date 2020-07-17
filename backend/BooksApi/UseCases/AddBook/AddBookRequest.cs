using System;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.AddBook
{
    public class AddBookRequest: IRequest<AbstractAnswer<Guid>>
    {
        public string BookTitle { get; set; }
        public int Year { get; set; }
        public string AuthorId { get; set; }
        public string GenreId { get; set; }
    }
}