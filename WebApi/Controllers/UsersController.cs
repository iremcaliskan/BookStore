using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration; // appsettings.js'ten veri almayi saglar

        public UsersController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new(_context, _mapper);
            command.Model = newUser;

            CreateUserCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new(_context, _configuration);
            command.Model = login;

            CreateTokenCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            var token = command.Handle();

            return token;
        }

        [HttpPost("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string refreshToken)
        {
            RefreshTokenCommand command = new(_context, _configuration);
            command.RefreshToken = refreshToken;

            RefreshTokenCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            var token = command.Handle();

            return token;
        }
    }
}