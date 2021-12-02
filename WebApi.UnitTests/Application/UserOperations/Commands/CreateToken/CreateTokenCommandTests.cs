using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests : IClassFixture<CommonTestFixture> // Attention please!
    {
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public CreateTokenCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _configuration = fixture.Configuration;
        }

        [Fact]
        public void Fact_WhenAlreadyExistUserWrongPasswordIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            CreateTokenCommand command = new(_context, _configuration);
            var user = new User() {Email = "demo@gmail.com", Password = "demo"};
            _context.Users.Add(user);
            _context.SaveChanges();

            command.Model = new CreateTokenModel() { Email = user.Email, Password = "user.Password"};

            // act & assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Email or Password is wrong!");
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            // arrange
            CreateTokenCommand command = new(_context, _configuration);
            var user = new User() { Email = "demo@gmail.com", Password = "demo" };
            _context.Users.Add(user);
            _context.SaveChanges();

            command.Model = new CreateTokenModel() { Email = user.Email, Password = user.Password };

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var addedUser = _context.Users.FirstOrDefault(x => x.Email == command.Model.Email);
            addedUser.Should().NotBeNull();
            addedUser.RefreshToken.Should().NotBeNull();
            addedUser.RefreshTokenExpireDate.Should().NotBeNull();
            addedUser.Email.Should().Be(command.Model.Email);
            addedUser.Password.Should().Be(command.Model.Password);
        }
    }
}