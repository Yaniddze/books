using System;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.Register
{
    public class RegisterRequest: IRequest<AbstractAnswer<Guid>>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}