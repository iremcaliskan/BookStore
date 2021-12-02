using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Fact_WhenAlreadyExistUserEmailIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            CreateUserCommand command = new(_context, _mapper);
            command.Model = new CreateUserModel() { Email = "test@test.com", Password = "12345"};

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The user is already in the system.");
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            // arrange
            CreateUserCommand command = new(_context, _mapper);
            command.Model = new CreateUserModel() { Email = "test1@test.com", Password = "12345" };

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var addedUser = _context.Users.FirstOrDefault(x => x.Email == command.Model.Email);
            addedUser.Should().NotBeNull();
            addedUser.Email.Should().Be(command.Model.Email);
            addedUser.Password.Should().Be(command.Model.Password);
        }
    }
}