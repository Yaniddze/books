using System.Collections.Generic;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.GetBooks
{
    public class GetBooksRequest: IRequest<AbstractAnswer<IEnumerable<Book>>>
    { }
}