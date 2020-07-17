using System.Collections.Generic;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.GetGenres
{
    public class GetGenresRequest: IRequest<AbstractAnswer<IEnumerable<Genre>>>
    {
        
    }
}