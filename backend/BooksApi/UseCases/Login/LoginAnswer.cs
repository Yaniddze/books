using System;
using System.Collections.Generic;

namespace BooksApi.UseCases.Login
{
    public class LoginAnswer
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Guid UserId { get; set; }
    }
}