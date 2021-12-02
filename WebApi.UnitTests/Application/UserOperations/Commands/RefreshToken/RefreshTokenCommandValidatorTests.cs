using FluentAssertions;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            RefreshTokenCommand command = new(null, null);
            command.RefreshToken = "";

            // act
            RefreshTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        //[InlineData("abc")]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string refreshToken)
        {
            // arrange
            RefreshTokenCommand command = new(null, null);
            command.RefreshToken = refreshToken;

            // act
            RefreshTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange
            RefreshTokenCommand command = new(null, null);
            command.RefreshToken = "refreshToken";

            // act
            RefreshTokenCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}