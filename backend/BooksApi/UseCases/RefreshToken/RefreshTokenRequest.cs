using System;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.RefreshToken
{
    public class RefreshTokenRequest: IRequest<AbstractAnswer<Guid>>
    {
        public Guid OldTokenId { get; set; }
    }
}