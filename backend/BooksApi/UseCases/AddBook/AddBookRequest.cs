using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.AddBook
{
    public class AddBookRequest: IRequest<AbstractAnswer<Book>>
    {
        public string BookTitle { get; set; }
        public int Year { get; set; }
        public string AuthorId { get; set; }
        public string GenreId { get; set; }
    }
}