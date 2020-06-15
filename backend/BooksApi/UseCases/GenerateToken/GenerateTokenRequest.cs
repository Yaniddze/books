using System;
using MediatR;

namespace BooksApi.UseCases.GenerateToken
{
    public class GenerateTokenRequest: IRequest<GenerateTokenAnswer>
    {
        public Guid UserId { get; set; }
    }
}
