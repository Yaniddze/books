using System.Collections.Generic;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.GetAuthors
{
    public class GetAuthorsRequest: IRequest<AbstractAnswer<IEnumerable<Author>>>
    { }
}