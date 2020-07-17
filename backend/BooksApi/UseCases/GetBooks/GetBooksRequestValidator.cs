using FluentValidation;

namespace BooksApi.UseCases.GetBooks
{
    public class GetBooksRequestValidator: AbstractValidator<GetBooksRequest>
    {
        public GetBooksRequestValidator()
        {
            
        }
    }
}