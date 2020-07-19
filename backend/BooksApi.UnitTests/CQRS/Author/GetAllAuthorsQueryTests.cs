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
    public class GetAllAuthorsQueryTests
    {
        private readonly IMapper _mapper;

        public GetAllAuthorsQueryTests()
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
        public async Task return_success()
        {
            // Arrange
            var generated = GenerateQuery(10);
            var testable = generated.Testable;
            var authors = generated.Authors;
            
            // Act
            var result = await testable.InvokeAsync();
            
            // Assert
            var excepted = authors.Select(x => _mapper.Map<AuthorDB, Entities.Author>(x)).ToList();
            Assert.Equal(excepted, result);
        }
        
        private GenerateQueryResult GenerateQuery(int entitiesCount)
        {
            var authors = Storage.GenerateAuthors(entitiesCount);

            var mockedContext = new Mock<IContext>();
            mockedContext.Setup(x => x.Authors).ReturnsDbSet(authors);

            var query = new GetAllAuthors(_mapper, mockedContext.Object);

            return new GenerateQueryResult
            {
                Authors = authors,
                Testable = query,
            };
        }

        private class GenerateQueryResult
        {
            public List<AuthorDB> Authors { get; set; }
            public IGetAllQuery<Entities.Author> Testable { get; set; }
        }
    }
}