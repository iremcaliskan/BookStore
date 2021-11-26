using FluentAssertions;
using System;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            UpdateAuthorCommand command = new(null, null);
            command.AuthorId = 0;
            command.Model = new UpdateAuthorModel() { FirstName = "", LastName = "", Birthdate = DateTime.Now};

            // act
            UpdateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] // Sample data
        [InlineData(0, "", "", "2021-11-26")]
        [InlineData(0, "A", "", "2021-11-26")]
        [InlineData(0, " ", "B", "2021-11-26")]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId, string firstname, string lastname, string birthdate)
        {
            // arrange
            UpdateAuthorCommand command = new(null, null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel() { FirstName = firstname, LastName = lastname, Birthdate = DateTime.Parse(birthdate)};

            // act
            UpdateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Fact_WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            UpdateAuthorCommand command = new(null, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel() { FirstName = "İrem", LastName = "Çalışkan", Birthdate = DateTime.Now};

            // act
            UpdateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange
            UpdateAuthorCommand command = new(null, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel() { FirstName = "İrem", LastName = "Çalışkan", Birthdate = DateTime.Now.AddYears(-16) };

            // act
            UpdateAuthorCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}
