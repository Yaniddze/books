using System;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.UseCases.Abstractions;
using BooksApi.UseCases.GenerateToken;
using BooksApi.UseCases.Login;
using BooksApi.UseCases.RefreshToken;
using BooksApi.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksApi.Controllers
{
    [Route("api/v1/identity")]
    public class IdentityController: Controller
    {
        private readonly IMediator _mediator;
//        private readonly ILogger _logger;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
//            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var loginResult = await _mediator.Send(request);
            var response = new AbstractAnswer<string>
            {
                Success = loginResult.Success,
                Errors = loginResult.Errors,
            };

            if (!loginResult.Success) return Ok(response);
            
            var tokenAnswer = await _mediator.Send(new GenerateTokenRequest {UserId = loginResult.Data});

//            response.Data = tokenAnswer.Data;

            Response.Cookies.Append(
                ".AspNetCore.Application.Id",
                tokenAnswer.Data,
                new CookieOptions{ MaxAge = TimeSpan.FromMinutes(60), Expires = DateTimeOffset.Now.AddMinutes(60)});
            
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var registerResult = await _mediator.Send(request);
            var response = new AbstractAnswer<string>
            {
                Success = registerResult.Success,
                Errors = registerResult.Errors,
            };

            if (!registerResult.Success) return Ok(response);
            
            var tokenAnswer = await _mediator.Send(new GenerateTokenRequest {UserId = registerResult.Data});

            response.Data = tokenAnswer.Data;

            return Ok(response);
        }

        [HttpPost("refresh")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RefreshAsync()
        {
            var tokenId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "token_id")?.Value;

            if (tokenId == null)
            {
                return Ok(new AbstractAnswer<string>
                {
                    Success = false,
                    Errors = new[] { "Bad token" }
                });
            }
            
            var refreshResponse = await _mediator.Send(new RefreshTokenRequest
            {
                OldTokenId = Guid.Parse(tokenId),
            });

            if (!refreshResponse.Success) return Ok(refreshResponse);
            
            var generateTokenResponse = await _mediator.Send(new GenerateTokenRequest
            {
                UserId = refreshResponse.Data,
            });

            return Ok(generateTokenResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            Response.Cookies.Delete(".AspNetCore.Application.Id");
            return Ok();
        }
    }
}