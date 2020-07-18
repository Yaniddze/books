using System;

namespace BooksApi.UseCases.GenerateToken
{
    public class GenerateTokenAnswer
    {
        public Guid RefreshId { get; set; }
        public string Token { get; set; }
    }
}