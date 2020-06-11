using System;
using System.Collections.Generic;

namespace BooksApi.UseCases.AddBook
{
    public class AddBookAnswer
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Guid BookId { get; set; }
    }
}