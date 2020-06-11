using BooksApi.Entities.Abstractions;

namespace BooksApi.Entities
{
    public class Book: Entity
    {
        public BookInfo BookInfo { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}
