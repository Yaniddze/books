using System;

namespace BooksApi.DataBase.Entities
{
    public class UserDB
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
