using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.Options;
using BooksApi.UseCases.Abstractions;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace BooksApi.UseCases.GenerateToken
{
    public class GenerateTokenUseCase: AbstractUseCase<GenerateTokenRequest, string>
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ICommandHandler<WriteTokenCommand> _commandHandler;
        private readonly ICommandHandler<DeactivateTokenCommand> _deactivateCommandHandler;

        public GenerateTokenUseCase(
            JwtOptions jwtOptions,
            ICommandHandler<WriteTokenCommand> commandHandler,
            IValidator<GenerateTokenRequest> validator, 
            ICommandHandler<DeactivateTokenCommand> deactivateCommandHandler
            ) 
            : base(validator)
        {
            _jwtOptions = jwtOptions;
            _commandHandler = commandHandler;
            _deactivateCommandHandler = deactivateCommandHandler;
        }

        protected override async Task<AbstractAnswer<string>> HandleAsync(
            GenerateTokenRequest request, CancellationToken cancellationToken
        )
        {
            var tokenId = Guid.NewGuid();
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", request.UserId.ToString()),
                }),
                // Lifetime: 5 minutes by default
                Expires = DateTime.UtcNow.AddMinutes(0.01),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.SecretInBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            await _deactivateCommandHandler.HandleAsync(new DeactivateTokenCommand
            {
                UserId = request.UserId,
            });
            
            var tempCommand = new WriteTokenCommand
            {
                Id = tokenId,
                Token = tokenHandler.WriteToken(token),
                UserId = request.UserId,
            };

            await _commandHandler.HandleAsync(tempCommand);

            return CreateSuccessAnswer(tempCommand.Token);
        }
    }
}