using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
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

        public GenerateTokenUseCase(
            JwtOptions jwtOptions,
            ICommandHandler<WriteTokenCommand> commandHandler,
            IValidator<GenerateTokenRequest> validator
        ) : base(validator)
        {
            _jwtOptions = jwtOptions;
            _commandHandler = commandHandler;
        }

        protected override async Task<AbstractAnswer<string>> HandleAsync(
            GenerateTokenRequest request, CancellationToken cancellationToken
        )
        {
            
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

            var tempCommand = new WriteTokenCommand
            {
                UserId = request.UserId,
                ExpiryDate = DateTime.UtcNow.AddYears(2),
                JwtId = token.Id,
                Token = tokenHandler.WriteToken(token),
            };

            await _commandHandler.HandleAsync(tempCommand);

            return CreateSuccessAnswer(tempCommand.Token);
        }
    }
}