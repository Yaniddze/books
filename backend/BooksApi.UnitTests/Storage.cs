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
        
        public static List<GenreDB> GenerateGenres(int count)
        {
            return Fixture.Build<GenreDB>()
                .With(x => x.Books, new List<BookDB>())
                .CreateMany(count)
                .ToList();
        }
        
        public static List<BookDB> GenerateBooks(int count, AuthorDB author, GenreDB genre)
        {
            return Fixture.Build<BookDB>()
                .With(x => x.Author, author)
                .With(x => x.AuthorId, author.Id)
                .With(x => x.Genre, genre)
                .With(x => x.GenreId, genre.Id)
                .CreateMany(count)
                .ToList();
        }
    }
}