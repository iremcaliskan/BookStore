using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void Theory_WhenInvalidIdsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = genreId;

            // act
            GetGenreDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = 0;

            // act
            GetGenreDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = 1;

            // act
            GetGenreDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}