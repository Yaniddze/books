using System.Collections.Generic;
using BooksApi.Entities.Abstractions;

namespace BooksApi.Entities
{
    public class BookInfo: ValueObject
    {
        public string Title { get; set; }
        public int Year { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Title;
            yield return Year;
        }
    }
}