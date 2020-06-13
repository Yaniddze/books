using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using MediatR;

namespace BooksApi.UseCases.GetGenres
{
    public class GetGenresUseCase: IRequestHandler<GetGenresRequest, GetGenresAnswer>
    {
        private readonly IGetAllQuery<Genre> _genreGetter;

        public GetGenresUseCase(IGetAllQuery<Genre> genreGetter)
        {
            _genreGetter = genreGetter;
        }

        public async Task<GetGenresAnswer> Handle(GetGenresRequest request, CancellationToken cancellationToken)
        {
            return new GetGenresAnswer
            {
                Success = true,
                Genres = await _genreGetter.InvokeAsync(),
            };
        }
    }
}