using MediatR;

namespace BooksApi.UseCases.Register
{
    public class RegisterRequest: IRequest<RegisterAnswer>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}