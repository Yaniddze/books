using System.Threading.Tasks;
using BooksApi.UseCases.AddBook;
using BooksApi.UseCases.DeleteBooks;
using BooksApi.UseCases.GetBooks;
using BooksApi.UseCases.UpdateBook;
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

        [HttpPut("add")]
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

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteBooksRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateBookRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}