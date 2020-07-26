using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using MediatR;

namespace BooksApi.DataBase.CQRS.TokenImpl
{
    public class WriteTokenCommandHandler: IRequestHandler<WriteTokenCommand>
    {
        private readonly IContext _context;

        public WriteTokenCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(WriteTokenCommand request, CancellationToken cancellationToken)
        {
            _context.Tokens.Add(new TokenDB
            {
                Id = request.Id,
                UserId = request.UserId,
                Active = true,
            });

            await _context.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}