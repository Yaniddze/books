using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.UseCases.Abstractions;
using BooksApi.UseCases.Login;
using BooksApi.UseCases.RefreshToken;
using BooksApi.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksApi.Controllers
{
    [Route("api/v1/identity")]
    public class IdentityController: Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(IMediator mediator, ILogger<IdentityController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var loginResult = await _mediator.Send(request);
            var response = new AbstractAnswer
            {
                Success = loginResult.Success,
                Errors = loginResult.Errors,
            };

            if (!loginResult.Success) return Ok(response);

            var generateTokenResult = await GenerateToken(loginResult.Data);

            var principal = GeneratePrincipal(loginResult.Data, generateTokenResult);

            _logger.LogInformation(
                $"user {request.Login} login and entered with refresh id {generateTokenResult}");
            
            await HttpContext.SignInAsync(principal);
            
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var registerResult = await _mediator.Send(request);
            var response = new AbstractAnswer
            {
                Success = registerResult.Success,
                Errors = registerResult.Errors,
            };

            if (!registerResult.Success) return Ok(response);

            await _mediator.Send(new AddUserCommand
            {
                UserToAdd = registerResult.Data,
            });

            var tokenAnswer = await GenerateToken(registerResult.Data.Id);

            var principal = GeneratePrincipal(registerResult.Data.Id, tokenAnswer);

            await HttpContext.SignInAsync(principal);
            
            _logger.LogInformation(
                $"user {request.Login} registered and entered with refresh id {tokenAnswer}");
            
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            var cookedPrincipal = HttpContext.User;

            var refreshInCookie = cookedPrincipal.Claims.FirstOrDefault(x => x.Type == "refreshId")?.Value;
            var userInCookie = cookedPrincipal.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

            Guid.TryParse(refreshInCookie ?? "", out var mappedRefreshId);
            Guid.TryParse(userInCookie ?? "", out var mappedUserId);

            if (mappedRefreshId == Guid.Empty || mappedUserId == Guid.Empty)
            {
                return Ok(new AbstractAnswer
                {
                    Success = false,
                    Errors = new []{ "Bad cookies" }
                });
            }
            
            var refreshResponse = await _mediator.Send(new RefreshTokenRequest
            {
                RefreshTokenId = mappedRefreshId,
            });

            if (!refreshResponse.Success)
            {
                return Ok(new AbstractAnswer
                {
                    Success = false,
                    Errors = refreshResponse.Errors,
                });
            }

            var generateTokenResponse = await GenerateToken(refreshResponse.Data);
            
            var principal = GeneratePrincipal(mappedUserId, generateTokenResponse);

            await HttpContext.SignInAsync(principal);
            
            _logger.LogInformation(
                $"refresh id {refreshInCookie} refreshed with to {generateTokenResponse}");
            
            return Ok(new AbstractAnswer
            {
                Success = true,
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            
            return Ok(new AbstractAnswer
            {
                Success = true,
            });
        }

        private static ClaimsPrincipal GeneratePrincipal(Guid userId, Guid refreshId)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", userId.ToString()),
                new Claim("refreshId", refreshId.ToString()),
            };

            var identity = new ClaimsIdentity(claims, "User Identity");

            var principal = new ClaimsPrincipal(new[] {identity});

            return principal;
        }

        private async Task<Guid> GenerateToken(Guid userId)
        {
            var tokenId = Guid.NewGuid();
            
            await _mediator.Send(new DeactivateTokenCommand
            {
                UserId = userId,
            });
            
            var tempCommand = new WriteTokenCommand
            {
                Id = tokenId,
                UserId = userId,
            };

            await _mediator.Send(tempCommand);

            return tokenId;
        }
    }
}