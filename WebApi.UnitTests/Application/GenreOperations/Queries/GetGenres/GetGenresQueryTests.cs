using AutoMapper;
using FluentAssertions;
using System.Linq;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact] // Happy Path
        public void Fact_WhenModelListCountEqualsEntityListCount_ShouldBeReturnSuccess()
        {
            // arrange
            GetGenresQuery query = new(_context, _mapper);

            // act
            var queryList = FluentActions.Invoking(() => query.Handle()).Invoke().Count;

            // assert
            var entityList = _context.Books.ToList().Count;

            Assert.Equal(queryList, entityList);
        }
    }
}