using System.Collections.Generic;
using BooksApi.Entities;

namespace BooksApi.UseCases.GetBooks
{
    public class GetBooksAnswer
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}