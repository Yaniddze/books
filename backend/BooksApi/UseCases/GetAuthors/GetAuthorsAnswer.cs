using System.Collections.Generic;
using BooksApi.Entities;

namespace BooksApi.UseCases.GetAuthors
{
    public class GetAuthorsAnswer
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}