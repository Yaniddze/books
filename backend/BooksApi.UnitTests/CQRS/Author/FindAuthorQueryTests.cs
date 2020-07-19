using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using BooksApi.CQRS.Queries;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.CQRS.AuthorImpl;
using BooksApi.DataBase.Entities;
using BooksApi.Installers.AutoMapper;
using BooksApi.Installers.AutoMapper.Mappers;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace BooksApi.UnitTests.CQRS.Author
{
    public class FindAuthorQueryTests
    {
        private readonly IMapper _mapper;

        public FindAuthorQueryTests()
        {
            var mapperConfiguration = new MapperConfiguration(x =>
            {
                x.AddExpressionMapping();

                var installers = new List<IMapperInstaller>
                {
                    new AuthorMapperInstaller(),
                };
                
                installers.ForEach(installer => installer.InstallMapper(x));
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task returned_one_success()
        {
            // Arrange
            var generatedQuery = GenerateQuery(5);
            var getter = generatedQuery.Testable;
            var authors = generatedQuery.Authors;
            var nameToFound = authors[0].Name;
            
            // Act
            var result = await getter.FindOneAsync(x => x.Name == nameToFound);
            
            // Assert
            var excepted = _mapper.Map<AuthorDB, Entities.Author>(authors.First(x => x.Name == nameToFound));
            Assert.Equal(excepted, result);
        }

        private GenerateQueryResult GenerateQuery(int entitiesCount)
        {
            var authors = Storage.GenerateAuthors(entitiesCount);

            var mockedContext = new Mock<IContext>();
            mockedContext.Setup(x => x.Authors).ReturnsDbSet(authors);

            var query = new FindAuthorQuery(_mapper, mockedContext.Object);

            return new GenerateQueryResult
            {
                Authors = authors,
                Testable = query,
            };
        }

        private class GenerateQueryResult
        {
            public List<AuthorDB> Authors { get; set; }
            public IFindQuery<Entities.Author> Testable { get; set; }
        }
    }
}