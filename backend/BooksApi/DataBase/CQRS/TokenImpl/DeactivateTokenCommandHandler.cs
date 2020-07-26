using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using MediatR;
using Z.EntityFramework.Plus;

namespace BooksApi.DataBase.CQRS.TokenImpl
{
    public class DeactivateTokenCommandHandler: IRequestHandler<DeactivateTokenCommand>
    {
        private readonly IContext _context;

        public DeactivateTokenCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeactivateTokenCommand request, CancellationToken cancellationToken)
        {
            await _context.Tokens
                .Where(x => x.UserId == request.UserId)
                .UpdateAsync(x => new TokenDB
                {
                    Active = false,
                }, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}