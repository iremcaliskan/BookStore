using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            DeleteAuthorCommand command = new(null); // Delete process is not important in here
            command.AuthorId = 0;

            // act
            DeleteAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            // arrange
            DeleteAuthorCommand command = new(null); // Delete process is not important in here
            command.AuthorId = authorId;

            // act
            DeleteAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputIsGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange
            DeleteAuthorCommand command = new(null);
            command.AuthorId = 1;

            // act
            DeleteAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }

        [Theory] // Sample data + Happy Path
        [InlineData(1)]
        [InlineData(16)]
        [InlineData(66)]
        [InlineData(100)]
        public void Theory_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess(int authorId)
        {
            // arrange
            DeleteAuthorCommand command = new(null); // Delete process is not important in here
            command.AuthorId = authorId;

            // act
            DeleteAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}