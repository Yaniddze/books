using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.GetBooks
{
    public class GetBooksUseCase: AbstractUseCase<GetBooksRequest, IEnumerable<Book>>
    {
        private readonly IGetAllQuery<Book> _bookQuery;

        public GetBooksUseCase(IGetAllQuery<Book> bookQuery, IValidator<GetBooksRequest> validator)
        : base(validator)
        {
            _bookQuery = bookQuery;
        }

        protected override async Task<AbstractAnswer<IEnumerable<Book>>> HandleAsync(GetBooksRequest request, CancellationToken cancellationToken)
        {
            return CreateSuccessAnswer(await _bookQuery.InvokeAsync());
        }
    }
}