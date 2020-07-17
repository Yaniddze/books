using System;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;

namespace BooksApi.DataBase.CQRS.TokenImpl
{
    public class WriteTokenCommandHandler: ICommandHandler<WriteTokenCommand>
    {
        private readonly IContext _context;

        public WriteTokenCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(WriteTokenCommand handled)
        {
            await _context.Tokens.AddAsync(new TokenDB
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                ExpiryDate = handled.ExpiryDate,
                JwtId = handled.JwtId,
                TokenValue = handled.Token,
                UserId = handled.UserId,
            });
        }
    }
}