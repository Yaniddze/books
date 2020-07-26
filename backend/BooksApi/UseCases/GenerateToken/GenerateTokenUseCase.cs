using System;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.GenerateToken
{
    public class GenerateTokenUseCase: AbstractUseCase<GenerateTokenRequest, Guid>
    {
        private readonly ICommandHandler<WriteTokenCommand> _commandHandler;
        private readonly ICommandHandler<DeactivateTokenCommand> _deactivateCommandHandler;

        public GenerateTokenUseCase(
            ICommandHandler<WriteTokenCommand> commandHandler,
            IValidator<GenerateTokenRequest> validator, 
            ICommandHandler<DeactivateTokenCommand> deactivateCommandHandler
            ) 
            : base(validator)
        {
            _commandHandler = commandHandler;
            _deactivateCommandHandler = deactivateCommandHandler;
        }

        protected override async Task<AbstractAnswer<Guid>> HandleAsync(
            GenerateTokenRequest request, CancellationToken cancellationToken
        )
        {
            var tokenId = Guid.NewGuid();
            
            await _deactivateCommandHandler.HandleAsync(new DeactivateTokenCommand
            {
                UserId = request.UserId,
            });
            
            var tempCommand = new WriteTokenCommand
            {
                Id = tokenId,
                UserId = request.UserId,
            };

            await _commandHandler.HandleAsync(tempCommand);

            return CreateSuccessAnswer(tokenId);
        }
    }
}