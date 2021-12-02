using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        } // https://localhost:44392/api/Authors

        [HttpGet("{id}")] // https://localhost:44392/api/Authors/1
        public IActionResult GetById([FromRoute] int id)
        {
            AuthorDetailViewModel result;
            GetAuthorDetailQuery query = new(_context, _mapper);
            query.AuthorId = id;

            GetAuthorDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query); // Middleware katmanı hatayı yakalayıp döndürecek try-catch'e gerek kalmadı

            result = query.Handle();
            return Ok(result);
        }

        [HttpPost] // https://localhost:44392/api/Authors
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new(_context, _mapper);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command); // Middleware katmanı hatayı yakalayıp döndürecek try-catch'e gerek kalmadı

            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")] // https://localhost:44392/api/Authors/5
        public IActionResult UpdateAuthor([FromRoute] int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new(_context, _mapper);
            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command); // Middleware katmanı hatayı yakalayıp döndürecek try-catch'e gerek kalmadı

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")] // https://localhost:44392/api/Authors/5
        public IActionResult DeleteAuthor([FromRoute] int id)
        {
            DeleteAuthorCommand command = new(_context);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command); // Middleware katmanı hatayı yakalayıp döndürecek try-catch'e gerek kalmadı

            command.Handle();
            return Ok();
        }
    }
}