using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.GetGenres
{
    public class GetGenresUseCase: AbstractUseCase<GetGenresRequest, IEnumerable<Genre>>
    {
        private readonly IGetAllQuery<Genre> _genreGetter;

        public GetGenresUseCase(IGetAllQuery<Genre> genreGetter, IValidator<GetGenresRequest> validator)
        : base(validator)
        {
            _genreGetter = genreGetter;
        }

        protected override async Task<AbstractAnswer<IEnumerable<Genre>>> HandleAsync(GetGenresRequest request, CancellationToken cancellationToken)
        {
            return CreateSuccessAnswer(await _genreGetter.InvokeAsync());
        }
    }
}