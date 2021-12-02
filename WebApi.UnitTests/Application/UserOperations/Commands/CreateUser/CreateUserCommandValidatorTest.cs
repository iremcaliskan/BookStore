using FluentAssertions;
using WebApi.Application.UserOperations.Commands.CreateUser;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTest
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            CreateUserCommand command = new(null, null);
            command.Model = new CreateUserModel() { Email = " ", Password = " " };

            // act
            CreateUserCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("", "", "", "")]
        [InlineData(" ", " ", " ", " ")]
        [InlineData(null, null, "email", "")]
        [InlineData(null, null, " ", "password")]
        //[InlineData(null, null, "email", "password")] passed
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, string email, string password)
        {
            // arrange
            CreateUserCommand command = new(null, null);
            command.Model = new CreateUserModel() { Name = name, Surname = surname, Email = email, Password = password };

            // act
            CreateUserCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange
            CreateUserCommand command = new(null, null);
            command.Model = new CreateUserModel() { Email = "mail", Password = "password"};

            // act
            CreateUserCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}