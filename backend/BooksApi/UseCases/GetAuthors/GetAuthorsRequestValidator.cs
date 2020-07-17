using FluentValidation;

namespace BooksApi.UseCases.GetAuthors
{
    public class GetAuthorsRequestValidator: AbstractValidator<GetAuthorsRequest>
    {
        public GetAuthorsRequestValidator()
        {
            
        }
    }
}