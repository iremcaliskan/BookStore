using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange (preparation) -- hazirlama
            UpdateGenreCommand command = new(null, null); // Update process is not important in here
            command.GenreId = 0; // > 0
            command.Model = new UpdateGenreModel() { Name = "Gen" }; // Min Length is 4

            // act (run) -- calistirma
            UpdateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] // Sample data
        [InlineData(0, null)]
        [InlineData(0, "")]
        [InlineData(0, "N")]
        [InlineData(0, "Na")]
        [InlineData(0, "Nam")]
        [InlineData(0, "Name")]
        [InlineData(1, null)]
        [InlineData(1, "")]
        [InlineData(1, "N")]
        [InlineData(1, "Na")]
        [InlineData(1, "Nam")]
        //[InlineData(1, "Name")] passed
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string name)
        {
            // arrange (preparation) -- hazirlama
            UpdateGenreCommand command = new(null, null); // Update process is not important in here
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel() { Name = name };

            // act (run) -- calistirma
            UpdateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputIsGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange (preparation) -- hazirlama
            UpdateGenreCommand command = new(null, null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel() { Name = "Updated Genre" };

            // act (run) -- calistirma
            UpdateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().Be(0);
        }

        [Theory] // Sample data + Happy Path
        [InlineData(1, "Genre 1")]
        [InlineData(16, "Genre 2")]
        [InlineData(66, "Genre 3")]
        [InlineData(100, "Genre 4")]
        public void Theory_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess(int genreId, string name)
        {
            // arrange (preparation) -- hazirlama
            UpdateGenreCommand command = new(null, null); // Update process is not important in here
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel() {Name = name};

            // act (run) -- calistirma
            UpdateGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}