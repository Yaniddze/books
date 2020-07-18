using System;
using BooksApi.Entities.Abstractions;

namespace BooksApi.Entities
{
    public class Token: Entity
    {
        public string TokenValue { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public string JwtId { get; set; }
    }
}