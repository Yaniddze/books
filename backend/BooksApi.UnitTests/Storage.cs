using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoFixture;
using BooksApi.DataBase.Entities;

namespace BooksApi.UnitTests
{
    public class Storage
    {
        private static readonly Fixture Fixture = new Fixture();
        static Storage()
        {
            Fixture.Register<ExtensionDataObject>(() => null);
        }

        public static List<AuthorDB> GenerateAuthors(int count)
        {
            return Fixture.Build<AuthorDB>()
                .With(x => x.Books, new List<BookDB>())
                .CreateMany(count)
                .ToList();
        }
    }
}