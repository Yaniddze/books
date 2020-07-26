using BooksApi.Entities;
using MediatR;

namespace BooksApi.CQRS.Commands
{
    public class AddBookCommand: IRequest
    {
        public Book BookToAdd { get; set; }
    }
}