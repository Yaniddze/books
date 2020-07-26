using System;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Controllers.Abstractions;
using BooksApi.CQRS.Commands;
using BooksApi.UseCases.Abstractions;
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
        private readonly IMapper _mapper;

        public BookController(IMediator mediator, ILogger<BookController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPut("add")]
        public async Task<IActionResult> AddAsync([FromBody] AddBookRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success) return Ok(result);

            await _mediator.Send(new AddBookCommand
            {
                BookToAdd = result.Data,
            });
            
            _logger.LogInformation($"book added with id {result.Data.Id}");
            
            return Ok(new AbstractAnswer<Guid>
            {
                Success = true,
                Data = result.Data.Id,
            });
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

            if (!result.Success) return Ok(result);

            await _mediator.Send(new DeleteBooksCommand
            {
                BookIds = request.BookIds
            });
            
            foreach (var requestBookId in request.BookIds)
            {
                _logger.LogInformation($"book deleted with id {requestBookId}");
            }
            
            return Ok(result);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateBookRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success) return Ok(result);

            var mappedCommand = _mapper.Map<UpdateBookRequest, UpdateBookCommand>(request);

            await _mediator.Send(mappedCommand);
            
            _logger.LogInformation($"book updated with id {request.BookId}");
            
            return Ok(result);
        }
    }
}