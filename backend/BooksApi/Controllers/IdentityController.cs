using System;
using System.Threading.Tasks;
using BooksApi.UseCases.Abstractions;
using BooksApi.UseCases.GenerateToken;
using BooksApi.UseCases.Login;
using BooksApi.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/v1/identity")]
    public class IdentityController: Controller
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
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
            
            return Ok(response);
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

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Application.Id");
            return Ok();
        }
    }
}