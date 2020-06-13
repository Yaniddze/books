using System.Collections.Generic;
using BooksApi.Entities;

namespace BooksApi.UseCases.GetGenres
{
    public class GetGenresAnswer
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}