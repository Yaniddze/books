using System;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.Entities;

namespace BooksApi.CQRS.Commands
{
    public class UpdateBookCommand: ICommand
    {
        public Guid OldBookId { get; set; }
        public Book NewBook { get; set; }
    }
}