using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _configuration = fixture.Configuration;
        }

        [Fact]
        public void Fact_WhenAlreadyExistUserWrongRefreshTokenIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            RefreshTokenCommand command = new(_context, _configuration);
            var user = new User() { Email = "demo@gmail.com", Password = "demo", RefreshToken = "demorefreshtoken", RefreshTokenExpireDate = new DateTime(2021,12,11)};
            _context.Users.Add(user);
            _context.SaveChanges();

            command.RefreshToken = "dxsadsadsadsa";

            // act & assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Refresh token is not valid!");
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            // arrange
            RefreshTokenCommand command = new(_context, _configuration);
            var user = new User() { Email = "demo@gmail.com", Password = "demo", RefreshToken = "demorefreshtoken", RefreshTokenExpireDate = new DateTime(2021, 12, 11) };
            _context.Users.Add(user);
            _context.SaveChanges();

            command.RefreshToken = "demorefreshtoken";

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var addedUser = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            addedUser.Should().NotBeNull();
            addedUser.RefreshToken.Should().NotBeNull();
            addedUser.RefreshTokenExpireDate.Should().NotBeNull();
            addedUser.RefreshToken.Should().NotBe(command.RefreshToken);
        }
    }
}