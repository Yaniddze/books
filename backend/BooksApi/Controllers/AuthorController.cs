using System.Threading.Tasks;
using BooksApi.Controllers.Abstractions;
using BooksApi.UseCases.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/v1/author")]
    public class AuthorController: AuthorizedController
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAuthorsRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}