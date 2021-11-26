using FluentAssertions;
using System;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel() { FirstName = "", LastName = "", Birthdate = DateTime.Now };

            // act
            CreateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("A", "B", "2021-11-25")]
        [InlineData("", "", "2021-11-25")]
        [InlineData(null, null, "2021-11-25")]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName, string birthdate)
        {
            // arrange
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel() { FirstName = firstName, LastName = lastName, Birthdate = DateTime.Parse(birthdate) };

            // act
            CreateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel() { FirstName = "Name", LastName = "Surname", Birthdate = DateTime.Now.Date };

            // act
            CreateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel() { FirstName = "Name", LastName = "Surname", Birthdate = DateTime.Now.AddYears(-5) };

            // act
            CreateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (Dogrulama)
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}