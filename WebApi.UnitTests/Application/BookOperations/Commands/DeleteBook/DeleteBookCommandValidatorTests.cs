using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange (preparation) -- hazirlama
            DeleteBookCommand command = new(null); // Delete process is not important in here
            command.BookId = 0;

            // act (run) -- calistirma
            DeleteBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] // Sample data
        [InlineData(0)]
        [InlineData(-1)]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            // arrange (preparation) -- hazirlama
            DeleteBookCommand command = new(null); // Delete process is not important in here
            command.BookId = bookId;

            // act (run) -- calistirma
            DeleteBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputIsGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange (preparation) -- hazirlama
            DeleteBookCommand command = new(null);
            command.BookId = 1;

            // act (run) -- calistirma
            DeleteBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().Be(0);
        }

        [Theory] // Sample data + Happy Path
        [InlineData(1)]
        [InlineData(16)]
        [InlineData(66)]
        [InlineData(100)]
        public void Theory_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess(int bookId)
        {
            // arrange (preparation) -- hazirlama
            DeleteBookCommand command = new(null); // Delete process is not important in here
            command.BookId = bookId;

            // act (run) -- calistirma
            DeleteBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}