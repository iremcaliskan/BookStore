using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void Theory_WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            // arrange
            GetBookDetailQuery query = new(null, null);
            query.BookId = bookId;

            // act
            GetBookDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            GetBookDetailQuery query = new(null, null);
            query.BookId = 0;

            // act
            GetBookDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenInvalidIdIsGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange
            GetBookDetailQuery query = new(null, null);
            query.BookId = 1;

            // act
            GetBookDetailQueryValidator validator = new();
            var validationResult = validator.Validate(query);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}