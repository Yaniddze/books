using System;

namespace BooksApi.DataBase.Entities
{
    public class TokenDB
    {
        public Guid Id { get; set; }
        public string TokenValue { get; set; }
        public bool Active { get; set; }
    }
}