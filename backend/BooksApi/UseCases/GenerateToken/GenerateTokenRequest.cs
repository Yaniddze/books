using System;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.GenerateToken
{
    public class GenerateTokenRequest: IRequest<AbstractAnswer<string>>
    {
        public Guid UserId { get; set; }
    }
}
