using System;
using System.Threading.Tasks;
using BooksApi.UseCases.Abstractions;
using BooksApi.UseCases.GenerateToken;
using BooksApi.UseCases.Login;
using BooksApi.UseCases.RefreshToken;
using BooksApi.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
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
            
            var tokenAnswer = await _mediator.Send(new GenerateTokenRequest {UserId = loginResult.Data});

            AddAuthCookies(tokenAnswer.Data.Token, tokenAnswer.Data.RefreshId.ToString());
            
            _logger.LogInformation(
                $"user {request.Login} has entered with refresh id ${tokenAnswer.Data.RefreshId}");

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
            
            AddAuthCookies(tokenAnswer.Data.Token, tokenAnswer.Data.RefreshId.ToString());
            
            _logger.LogInformation(
                $"user ${request.Login} registered and entered with refresh id ${tokenAnswer.Data.RefreshId}");
            
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            Request.Cookies.TryGetValue(".AspNetCore.Application.Refresh", out var valueInCookie);

            if (valueInCookie == null)
            {
                return Ok(new AbstractAnswer
                {
                    Success = false,
                    Errors = new []{ "There is no refresh cookie" }
                });
            }

            var refreshTokenId = Guid.Parse(valueInCookie);

            var refreshResponse = await _mediator.Send(new RefreshTokenRequest
            {
                RefreshTokenId = refreshTokenId,
            });

            if (!refreshResponse.Success)
            {
                DeleteAuthCookies();
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

            AddAuthCookies(generateTokenResponse.Data.Token, generateTokenResponse.Data.RefreshId.ToString());
            
            _logger.LogInformation(
                $"refresh id ${valueInCookie} refreshed with to ${generateTokenResponse.Data.RefreshId}");
            
            return Ok(new AbstractAnswer
            {
                Success = true,
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            DeleteAuthCookies();
            return Ok(new AbstractAnswer
            {
                Success = true,
            });
        }

        private void AddAuthCookies(string token, string refreshId)
        {
            Response.Cookies.Append(
                ".AspNetCore.Application.Id",
                token,
                new CookieOptions{ MaxAge = TimeSpan.FromMinutes(60), Expires = DateTimeOffset.Now.AddMinutes(60)});
            
            Response.Cookies.Append(
                ".AspNetCore.Application.Refresh",
                refreshId,
                new CookieOptions{ MaxAge = TimeSpan.FromMinutes(60), Expires = DateTimeOffset.Now.AddMinutes(60)});
        }

        private void DeleteAuthCookies()
        {
            Response.Cookies.Delete(".AspNetCore.Application.Id");
            Response.Cookies.Delete(".AspNetCore.Application.Refresh");

        }
    }
}