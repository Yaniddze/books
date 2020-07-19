using System.Threading.Tasks;
using BooksApi.Controllers.Abstractions;
using BooksApi.UseCases.AddBook;
using BooksApi.UseCases.DeleteBooks;
using BooksApi.UseCases.GetBooks;
using BooksApi.UseCases.UpdateBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksApi.Controllers
{
    [Route("api/v1/book")]
    public class BookController: AuthorizedController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPut("add")]
        public async Task<IActionResult> AddAsync([FromBody] AddBookRequest request)
        {
            var result = await _mediator.Send(request);
            
            _logger.LogInformation($"book added with id {result.Data}");
            
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
            
            foreach (var requestBookId in request.BookIds)
            {
                _logger.LogInformation($"book deleted with id ${requestBookId}");
            }
            
            return Ok(result);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateBookRequest request)
        {
            var result = await _mediator.Send(request);
            
            _logger.LogInformation($"book updated with id ${request.BookId}");
            
            return Ok(result);
        }
    }
}