using System;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.GenerateToken
{
    public class GenerateTokenRequest: IRequest<AbstractAnswer<Guid>>
    {
        public Guid UserId { get; set; }
    }
}
