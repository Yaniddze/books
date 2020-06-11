using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using MediatR;

namespace BooksApi.UseCases.GetBooks
{
    public class GetBooksUseCase: IRequestHandler<GetBooksRequest, GetBooksAnswer>
    {
        private readonly IGetAllQuery<Book> _bookQuery;

        public GetBooksUseCase(IGetAllQuery<Book> bookQuery)
        {
            _bookQuery = bookQuery;
        }

        public async Task<GetBooksAnswer> Handle(GetBooksRequest request, CancellationToken cancellationToken)
        {
            return new GetBooksAnswer
            {
                Success = true,
                Books = await _bookQuery.InvokeAsync(),
            };
        }
    }
}