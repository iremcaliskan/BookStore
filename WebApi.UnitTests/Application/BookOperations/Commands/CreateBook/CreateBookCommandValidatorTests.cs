using FluentAssertions;
using System;
using WebApi.Application.BookOperations.Commands.CreateBook;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests
    {

        [Fact]
        public void Fact_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange (Hazırlama)
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel() { Title = "", PageCount = 0, PublishDate = DateTime.Now, GenreId = 0, AuthorId = 0 };

            // act (Çalıştırma)
            CreateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);
            // act & assert (Çalıştırma ve Doğrulama)

            // assert (Doğrulama)
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("", 0, 0, 0)]
        [InlineData("Boo", 0, 0, 0)]
        [InlineData("Book", 0, 0, 0)]
        [InlineData("Book", 0, 0, 1)]
        [InlineData("Book", 0, 1, 0)]
        [InlineData("Book", 0, 1, 1)]
        [InlineData("Book", 100, 0, 0)]
        [InlineData("Book", 100, 1, 0)]
        [InlineData("Book", 100, 0, 1)]
        //[InlineData("Book", 100, 1, 1)] -- pass
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            // arrange (Hazırlama)
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel() { Title = title, PageCount = pageCount, PublishDate = DateTime.Now.AddYears(-1), GenreId = genreId, AuthorId = authorId };

            // act (Çalıştırma)
            CreateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // act & assert (Çalıştırma ve Doğrulama)

            // assert (Doğrulama)
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Sadece DateTime kontrolü için diğer caseler doğru atanmalı
        public void WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {
            // arrange (Hazırlama)
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date, GenreId = 1, AuthorId = 1 };

            // act (Çalıştırma)
            CreateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // act & assert (Çalıştırma ve Doğrulama)

            // assert (Doğrulama)
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange (Hazırlama)
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookModel() { Title = "Book", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId = 1, AuthorId = 1 };

            // act (Çalıştırma)
            CreateBookCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // act & assert (Çalıştırma ve Doğrulama)

            // assert (Doğrulama)
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}