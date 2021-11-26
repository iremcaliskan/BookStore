using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void Theory_WhenNotExistedAuthorIdsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            // arrange
            UpdateAuthorCommand command = new(_context, _mapper);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel() { FirstName = "Updated N", LastName = "Updated S", Birthdate = DateTime.Now.AddYears(-5) };

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No author found to be updated!");
        }

        [Fact]
        public void Fact_WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            // arrange
            UpdateAuthorCommand command = new(_context, _mapper);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel() { FirstName = "Updated N", LastName = "Updated S", Birthdate = DateTime.Now.AddYears(-5) };

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var updatedBook = _context.Authors.FirstOrDefault(x => x.Id == command.AuthorId);
            updatedBook.Should().NotBeNull();
            updatedBook.FirstName.Should().Be(command.Model.FirstName);
            updatedBook.LastName.Should().Be(command.Model.LastName);
            updatedBook.Birthdate.Should().Be(command.Model.Birthdate);
        }

        [Fact]
        public void Fact_WhenValidDateTimeIsGiven_Author_ShouldBeUpdated()
        {
            // arrange
            UpdateAuthorCommand command = new(_context, _mapper);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel() { Birthdate = new DateTime(1990, 01, 01) };

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var updatedAuthor = _context.Authors.FirstOrDefault(x => x.Id == command.AuthorId);
            updatedAuthor.Should().NotBeNull();
            updatedAuthor.FirstName.Should().NotBe(command.Model.FirstName);
            updatedAuthor.LastName.Should().NotBe(command.Model.LastName);
            updatedAuthor.Birthdate.Should().Be(command.Model.Birthdate);
        }
    }
}