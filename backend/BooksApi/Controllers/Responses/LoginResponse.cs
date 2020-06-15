using System.Collections.Generic;

namespace BooksApi.Controllers.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Token { get; set; }
    }
}