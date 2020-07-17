using System;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.Login
{
    public class LoginRequest: IRequest<AbstractAnswer<Guid>>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}