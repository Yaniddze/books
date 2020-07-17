using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.GetAuthors
{
    public class GetAuthorsUseCase: AbstractUseCase<GetAuthorsRequest, IEnumerable<Author>>
    {
        private readonly IGetAllQuery<Author> _authorQuery;

        public GetAuthorsUseCase(IGetAllQuery<Author> authorQuery, IValidator<GetAuthorsRequest> validator)
        : base(validator)
        {
            _authorQuery = authorQuery;
        }

        protected override async Task<AbstractAnswer<IEnumerable<Author>>> HandleAsync(GetAuthorsRequest request, CancellationToken cancellationToken)
        {
            return CreateSuccessAnswer(await _authorQuery.InvokeAsync());
        }
    }
}