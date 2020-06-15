using System;
using System.Collections.Generic;

namespace BooksApi.UseCases.Register
{
    public class RegisterAnswer
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Guid UserId { get; set; }
    }
}