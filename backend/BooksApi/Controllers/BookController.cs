using System.Threading.Tasks;
using BooksApi.UseCases.AddBook;
using BooksApi.UseCases.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/v1/book")]
    public class BookController: Controller
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] AddBookRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> AllAsync([FromQuery] GetBooksRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}