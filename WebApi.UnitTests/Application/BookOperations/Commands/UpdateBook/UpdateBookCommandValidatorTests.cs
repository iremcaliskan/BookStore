using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange (Hazırlama)
            UpdateBookCommand command = new(null, null);
            command.BookId = 0;
            command.Model = new UpdateBookModel() { Title = "", PageCount = 0, PublishDate = DateTime.Now, GenreId = 0, AuthorId = 0 };

            // act (Çalıştırma)
            UpdateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (Doğrulama)
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] // Sample data
        [InlineData(0, "", 0, 0, 0)]
        [InlineData(0, "Boo", 0, 0, 0)]
        [InlineData(0, "Book", 0, 0, 0)]
        [InlineData(0, "Book", 0, 0, 1)]
        [InlineData(0, "Book", 0, 1, 0)]
        [InlineData(0, "Book", 0, 1, 1)]
        [InlineData(0, "Book", 100, 0, 0)]
        [InlineData(0, "Book", 100, 1, 0)]
        [InlineData(0, "Book", 100, 0, 1)]
        [InlineData(1, "", 0, 0, 0)]
        [InlineData(1, "Boo", 0, 0, 0)]
        [InlineData(1, "Book", 0, 0, 0)]
        [InlineData(1, "Book", 0, 0, 1)]
        [InlineData(1, "Book", 0, 1, 0)]
        [InlineData(1, "Book", 0, 1, 1)]
        [InlineData(1, "Book", 100, 0, 0)]
        [InlineData(1, "Book", 100, 1, 0)]
        [InlineData(1, "Book", 100, 0, 1)]
        //[InlineData(1, "Book", 100, 1, 1)] // passed
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int pageCount, int genreId, int authorId)
        {
            // arrange (Hazırlama)
            UpdateBookCommand command = new(null, null); // Update işlemi ile değil, doğrulama ile ilgileniyoruz.
            command.BookId = bookId;
            command.Model = new UpdateBookModel() { Title = title, PageCount = pageCount, PublishDate = DateTime.Now.AddYears(-1), GenreId = genreId, AuthorId = authorId };

            // act (Çalıştırma)
            UpdateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (Doğrulama)
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // For Datetime control, other values have to be valid
        public void WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {
            // arrange (Hazırlama)
            UpdateBookCommand command = new(null, null);
            command.BookId = 1;
            command.Model = new UpdateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date, GenreId = 1, AuthorId = 1 };

            // act (Çalıştırma)
            UpdateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (Doğrulama)
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange (Hazırlama)
            UpdateBookCommand command = new(null, null);
            command.Model = new UpdateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId = 1, AuthorId = 1 };

            // act (Çalıştırma)
            UpdateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (Doğrulama)
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}