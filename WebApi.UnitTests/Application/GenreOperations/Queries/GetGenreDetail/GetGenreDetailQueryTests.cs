using AutoMapper;
using FluentAssertions;
using System;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Fact_WhenNotExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = 0;

            // act & assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre is not found!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        [InlineData(999)]
        public void Theory_WhenNotExistGenreIdsAreGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            // arrange
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = genreId;

            // act & assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre is not found!");
        }

        [Fact] // Happy Path
        public void Fact_WhenExistGenreIdIsGiven_GenreDetails_ShouldBeReturn()
        {
            // arrange
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = 1;

            // act & assert
             var genreDetail = FluentActions.Invoking(() => query.Handle()).Invoke();
             genreDetail.Should().NotBeNull();
        }
    }
}