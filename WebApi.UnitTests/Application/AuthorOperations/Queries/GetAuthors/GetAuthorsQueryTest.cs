using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact] // Happy Path
        public void Fact_WhenModelListCountEqualsEntityListCount_ShouldBeReturnSuccess()
        {
            // arrange
            GetAuthorsQuery query = new(_context, _mapper);

            // act
            var queryList = FluentActions.Invoking(() => query.Handle()).Invoke().Count;

            // assert
            var entityList = _context.Authors.ToList().Count;

            Assert.Equal(queryList, entityList);
        }
    }
}