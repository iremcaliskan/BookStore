using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void Fact_WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var author = new Author {FirstName = "Name", LastName = "Lastname", Birthdate = new DateTime(1990, 1, 1)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new(_context, _mapper);
            command.Model = new CreateAuthorModel
                {FirstName = author.FirstName, LastName = author.LastName, Birthdate = author.Birthdate};

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("The author is already in the system.");
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // arrange
            CreateAuthorCommand command = new(_context, _mapper);
            command.Model = new CreateAuthorModel
                {FirstName = "Name", LastName = "Lastname", Birthdate = new DateTime(1990, 1, 1)};

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var addedAuthor = _context.Authors.FirstOrDefault(x =>
                x.FirstName == command.Model.FirstName && x.LastName == command.Model.LastName &&
                x.Birthdate == command.Model.Birthdate);
            addedAuthor.Should().NotBeNull();
            addedAuthor.FirstName.Should().Be(command.Model.FirstName);
            addedAuthor.LastName.Should().Be(command.Model.LastName);
            addedAuthor.Birthdate.Should().Be(command.Model.Birthdate);
        }

        [Theory]
        [InlineData("Name 1", "Lastname 1", "1990-1-1")]
        [InlineData("Name 2", "Lastname 2", "1992-2-2")]
        [InlineData("Name 3", "Lastname 3", "1992-3-3")]
        public void Theory_WhenValidInputsAreGiven_Author_ShouldBeCreated(string firstName, string lastName, string birthdate)
        {
            // arrange (preparation) -- hazirlama
            CreateAuthorCommand command = new(_context, _mapper);
            command.Model = new CreateAuthorModel
                {FirstName = firstName, LastName = lastName, Birthdate = DateTime.Parse(birthdate)};

            // act (run) -- calistirma
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert (defend) -- iddia etmek
            var addedAuthor = _context.Authors.FirstOrDefault(x => x.FirstName == command.Model.FirstName && x.LastName == command.Model.LastName && x.Birthdate == command.Model.Birthdate);
            addedAuthor.FirstName.Should().Be(command.Model.FirstName);
            addedAuthor.LastName.Should().Be(command.Model.LastName);
            addedAuthor.Birthdate.Should().Be(command.Model.Birthdate);
        }
    }
}