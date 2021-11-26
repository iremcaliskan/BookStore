using AutoMapper;
using FluentAssertions;
using System.Linq;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact] // Happy Path
        public void Fact_WhenModelListCountEqualsEntityListCount_ShouldBeReturnSuccess()
        {
            // arrange
            GetBooksQuery query = new(_context, _mapper);

            // act
            var queryList = FluentActions.Invoking(() => query.Handle()).Invoke().Count;

            // assert
            var entityList = _context.Books.ToList().Count;

            Assert.Equal(queryList, entityList);
        }
    }
}