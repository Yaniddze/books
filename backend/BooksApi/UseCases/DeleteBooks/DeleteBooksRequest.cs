using System;
using MediatR;

namespace BooksApi.UseCases.DeleteBooks
{
    public class DeleteBooksRequest: IRequest<DeleteBooksAnswer>
    {
        public Guid[] BookIds { get; set; }
    }
}