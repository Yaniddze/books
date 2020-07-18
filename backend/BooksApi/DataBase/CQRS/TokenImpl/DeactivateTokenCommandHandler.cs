using System.Linq;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using Z.EntityFramework.Plus;

namespace BooksApi.DataBase.CQRS.TokenImpl
{
    public class DeactivateTokenCommandHandler: ICommandHandler<DeactivateTokenCommand>
    {
        private readonly IContext _context;

        public DeactivateTokenCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(DeactivateTokenCommand handled)
        {
            await _context.Tokens
                .Where(x => x.Id == handled.TokenId)
                .UpdateAsync(x => new TokenDB
                {
                    Id = x.Id,
                    Active = false,
                    CreationDate = x.CreationDate,
                    ExpiryDate = x.ExpiryDate,
                    JwtId = x.JwtId,
                    TokenValue = x.TokenValue,
                    UserId = x.UserId,
                });
        }
    }
}