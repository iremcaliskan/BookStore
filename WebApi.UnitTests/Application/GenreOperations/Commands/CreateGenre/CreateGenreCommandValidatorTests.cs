using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange (preparation) -- hazirlama
            CreateGenreCommand command = new(null, null); // Process is not important in here
            command.Model = new CreateGenreModel() { Name = "" };

            // act (run) -- calistirma
            CreateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        //[InlineData("abcd")] passed
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            // arrange (preparation) -- hazirlama
            CreateGenreCommand command = new(null, null); // Process is not important in here
            command.Model = new CreateGenreModel() { Name = name };

            // act (run) -- calistirma
            CreateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        
        [Fact] // Happy Path
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange (preparation) -- hazirlama
            CreateGenreCommand command = new(null, null); // Process is not important in here
            command.Model = new CreateGenreModel() { Name = "Genre" };

            // act (run) -- calistirma
            CreateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}