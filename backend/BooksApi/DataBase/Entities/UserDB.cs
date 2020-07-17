using System;
using System.Collections.Generic;

namespace BooksApi.DataBase.Entities
{
    public class UserDB
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<TokenDB> Tokens { get; set; }
    }
}
