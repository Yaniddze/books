using BooksApi.Entities.Abstractions;

namespace BooksApi.Entities
{
    public class Token: Entity
    {
        public string TokenValue { get; set; }
        public bool Active { get; set; }
    }
}