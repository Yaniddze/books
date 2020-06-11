using System;
using System.Collections.Generic;

namespace BooksApi.DataBase.Entities
{
    public class AuthorDB
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<BookDB> Books { get; set; }
    }
}