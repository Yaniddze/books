using System;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using MediatR;

namespace BooksApi.UseCases.Register
{
    public class RegisterRequest: IRequest<AbstractAnswer<User>>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}