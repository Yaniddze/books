using System.Collections.Generic;
using BooksApi.Entities.Abstractions;

namespace BooksApi.Entities
{
    public class LoginInfo: ValueObject
    {
        public string Login { get; set; }
        public string Password { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Login;
            yield return Password;
        }
    }
}