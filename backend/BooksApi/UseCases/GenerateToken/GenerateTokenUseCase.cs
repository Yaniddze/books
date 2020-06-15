using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.Options;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace BooksApi.UseCases.GenerateToken
{
    public class GenerateTokenUseCase: IRequestHandler<GenerateTokenRequest, GenerateTokenAnswer>
    {
        private readonly JwtOptions _jwtOptions;

        public GenerateTokenUseCase(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public Task<GenerateTokenAnswer> Handle(GenerateTokenRequest request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", request.UserId.ToString()),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.SecretInBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(new GenerateTokenAnswer
            {
                Token = tokenHandler.WriteToken(token),
            });
        }
    }
}