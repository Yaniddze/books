using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.Entities;

namespace BooksApi.CQRS.Commands
{
    public class AddBookCommand: ICommand
    {
        public Book BookToAdd { get; set; }
    }
}