using System;

namespace BooksApi.DataBase.Entities
{
    public class BookDB
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Guid GenreId { get; set; }
        public GenreDB Genre { get; set; }
        public Guid AuthorId { get; set; }
        public AuthorDB Author { get; set; }
    }
}
