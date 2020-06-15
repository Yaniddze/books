using MediatR;

namespace BooksApi.UseCases.Login
{
    public class LoginRequest: IRequest<LoginAnswer>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}