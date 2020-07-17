using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.UpdateBook
{
    public class UpdateBookRequest: IRequest<AbstractAnswer>
    {
        public string BookId { get; set; }
        public int NewYear { get; set; }
        public string NewTitle { get; set; }
        public string NewAuthorId { get; set; }
        public string NewGenreId { get; set; }
    }
}