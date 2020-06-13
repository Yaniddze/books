using System.Threading.Tasks;
using BooksApi.UseCases.GetGenres;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/v1/genre")]
    public class GenreController: Controller
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetGenresRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}