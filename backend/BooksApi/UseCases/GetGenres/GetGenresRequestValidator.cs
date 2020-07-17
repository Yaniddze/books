using FluentValidation;

namespace BooksApi.UseCases.GetGenres
{
    public class GetGenresRequestValidator: AbstractValidator<GetGenresRequest>
    {
        public GetGenresRequestValidator()
        {
            
        }
    }
}