using System;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.DeleteBooks
{
    public class DeleteBooksRequest: IRequest<AbstractAnswer>
    {
        public Guid[] BookIds { get; set; }
    }
}