using System;

namespace BooksApi.DataBase.Entities
{
    public class TokenDB
    {
        public Guid Id { get; set; }
        public string TokenValue { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string JwtId { get; set; }
        public bool Active { get; set; }
        public UserDB User { get; set; }
    }
}