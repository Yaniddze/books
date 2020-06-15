using System.Threading.Tasks;
using BooksApi.Controllers.Responses;
using BooksApi.UseCases.GenerateToken;
using BooksApi.UseCases.Login;
using MediatR;
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
            var response = new LoginResponse
            {
                Success = loginResult.Success,
                Errors = loginResult.Errors,
            };

            if (!loginResult.Success) return Ok(response);
            
            var tokenAnswer = await _mediator.Send(new GenerateTokenRequest {UserId = loginResult.UserId});

            response.Token = tokenAnswer.Token;

            return Ok(response);
        }
    }
}