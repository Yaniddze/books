using System;
using System.Collections.Generic;

namespace BooksApi.DataBase.Entities
{
    public class GenreDB
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<BookDB> Books { get; set; }
    }
}