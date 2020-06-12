using System.Collections.Generic;

namespace BooksApi.UseCases.UpdateBook
{
    public class UpdateBookAnswer
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}