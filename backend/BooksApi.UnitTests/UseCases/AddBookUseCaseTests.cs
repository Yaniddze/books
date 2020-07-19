using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.CQRS.AuthorImpl;
using BooksApi.DataBase.CQRS.BookImpl;
using BooksApi.DataBase.CQRS.GenreImpl;
using BooksApi.DataBase.Entities;
using BooksApi.Installers.AutoMapper;
using BooksApi.Installers.AutoMapper.Mappers;
using BooksApi.UseCases.AddBook;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace BooksApi.UnitTests.UseCases
{
    public class AddBookUseCaseTests
    {
        private readonly IMapper _mapper;

        public AddBookUseCaseTests()
        {
            var mapperConfiguration = new MapperConfiguration(x =>
            {
                x.AddExpressionMapping();

                var installers = new List<IMapperInstaller>
                {
                    new AuthorMapperInstaller(),
                    new GenreMapperInstaller(),
                    new BookMapperInstaller(),
                };
                
                installers.ForEach(installer => installer.InstallMapper(x));
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task add_book_success()
        {
            // Arrange
            var generated = GenerateQuery(10);

            var title = "Book title";
            var year = 1920;
            var author = generated.Authors.Last();
            var genre = generated.Genres.Last();
            
            // Act
            var result = await generated.Testable.Handle(new AddBookRequest
            {
                BookTitle = title,
                Year = year,
                AuthorId = author.Id.ToString(),
                GenreId = genre.Id.ToString(),
            }, CancellationToken.None);
            
            // Assert
            Assert.True(result.Success);
        }
        
        private GenerateQueryResult GenerateQuery(int entitiesCount)
        {
            var authors = Storage.GenerateAuthors(entitiesCount);
            var genres = Storage.GenerateGenres(entitiesCount);
            var books = Storage.GenerateBooks(entitiesCount, authors[0], genres[0]);

            var mockedContext = new Mock<IContext>();
            mockedContext.Setup(x => x.Authors).ReturnsDbSet(authors);
            mockedContext.Setup(x => x.Books).ReturnsDbSet(books);
            mockedContext.Setup(x => x.Genres).ReturnsDbSet(genres);

            var context = mockedContext.Object;

            var addBookHandler = new AddBookCommandHandler(_mapper, context);
            var authorFinder = new FindAuthorQuery(_mapper, context);
            var genreFinder = new FindGenreQuery(_mapper, context);

            var query = new AddBookUseCase(addBookHandler, new AddBookRequestValidator(), authorFinder, genreFinder);

            return new GenerateQueryResult
            {
                Books = books,
                Authors = authors,
                Genres = genres,
                Testable = query,
            };
        }

        private class GenerateQueryResult
        {
            public List<BookDB> Books { get; set; }
            public List<AuthorDB> Authors { get; set; }
            public List<GenreDB> Genres { get; set; }
            public AddBookUseCase Testable { get; set; }
        }
    }
}