using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.RefreshToken
{
    public class RefreshTokenUseCase: AbstractUseCase<RefreshTokenRequest>
    {
        private readonly IFindQuery<Token> _finder;
        private readonly ICommandHandler<DeactivateTokenCommand> _commandHandler;
        
        public RefreshTokenUseCase(
            IValidator<RefreshTokenRequest> validator,
            IFindQuery<Token> finder,
            ICommandHandler<DeactivateTokenCommand> commandHandler
            ) 
            : base(validator)
        {
            _finder = finder;
            _commandHandler = commandHandler;
        }

        protected override async Task<AbstractAnswer> HandleAsync(
            RefreshTokenRequest request, CancellationToken cancellationToken
        )
        {
            var founded = await _finder.FindOneAsync(x => x.Id == request.OldTokenId);

            if (founded == null)
            {
                return CreateBadAnswer(new[] { "Your token doesn't exists" });
            }

            if (!founded.Active)
            {
                return CreateBadAnswer(new[] { "Token is not active" });
            }

            await _commandHandler.HandleAsync(new DeactivateTokenCommand
            {
                TokenId = founded.Id
            });

            return CreateSuccessAnswer();
        }
    }
}