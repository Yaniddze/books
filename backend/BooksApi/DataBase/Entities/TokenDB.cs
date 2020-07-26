using System;

namespace BooksApi.DataBase.Entities
{
    public class TokenDB
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public Guid UserId { get; set; }
        public UserDB User { get; set; }
    }
}