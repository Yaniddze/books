using BooksApi.Entities.Abstractions;

namespace BooksApi.Entities
{
    public class User: Entity
    {
        public LoginInfo LoginInfo { get; set; }
    }
}
