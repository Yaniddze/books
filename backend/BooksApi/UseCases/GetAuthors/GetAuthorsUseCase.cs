using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using MediatR;

namespace BooksApi.UseCases.GetAuthors
{
    public class GetAuthorsUseCase: IRequestHandler<GetAuthorsRequest, GetAuthorsAnswer>
    {
        private readonly IGetAllQuery<Author> _authorQuery;

        public GetAuthorsUseCase(IGetAllQuery<Author> authorQuery)
        {
            _authorQuery = authorQuery;
        }

        public async Task<GetAuthorsAnswer> Handle(GetAuthorsRequest request, CancellationToken cancellationToken)
        {
            return new GetAuthorsAnswer
            {
                Success = true,
                Authors = await _authorQuery.InvokeAsync(),
            };
        }
    }
}