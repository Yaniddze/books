using System.Collections.Generic;

namespace BooksApi.UseCases.DeleteBooks
{
    public class DeleteBooksAnswer
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}