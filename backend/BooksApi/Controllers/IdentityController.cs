using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BooksApi.UseCases.Abstractions;
using BooksApi.UseCases.GenerateToken;
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

            var generateTokenResult = await _mediator.Send(new GenerateTokenRequest
            {
                UserId = loginResult.Data
            });

            var principal = GeneratePrincipal(loginResult.Data, generateTokenResult.Data);

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
            
            var tokenAnswer = await _mediator.Send(new GenerateTokenRequest {UserId = registerResult.Data});

            var principal = GeneratePrincipal(registerResult.Data, tokenAnswer.Data);

            await HttpContext.SignInAsync(principal);
            
            _logger.LogInformation(
                $"user {request.Login} registered and entered with refresh id {tokenAnswer.Data}");
            
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

            var generateTokenResponse = await _mediator.Send(new GenerateTokenRequest
            {
                UserId = refreshResponse.Data,
            });
            
            var principal = GeneratePrincipal(mappedUserId, generateTokenResponse.Data);

            await HttpContext.SignInAsync(principal);
            
            _logger.LogInformation(
                $"refresh id {refreshInCookie} refreshed with to {generateTokenResponse.Data}");
            
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
    }
}