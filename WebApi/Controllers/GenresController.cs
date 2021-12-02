using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")] // api/Genres
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] // localhost:44392/api/Genres/1
        public IActionResult GetById([FromRoute] int id)
        {
            GenreDetailViewModel result;
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query); // Middleware katmanı hatayı yakalayıp döndürecek try-catch'e gerek kalmadı

            result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new(_context, _mapper);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command); // Middleware katmanı hatayı yakalayıp döndürecek try-catch'e gerek kalmadı

            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre([FromRoute] int id, [FromBody] UpdateGenreModel updatedGenre)
        {
            UpdateGenreCommand command = new(_context, _mapper);
            command.GenreId = id;
            command.Model = updatedGenre;

            UpdateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command); // Middleware katmanı hatayı yakalayıp döndürecek try-catch'e gerek kalmadı

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre([FromRoute] int id)
        {
            DeleteGenreCommand command = new(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command); // Middleware katmanı hatayı yakalayıp döndürecek try-catch'e gerek kalmadı

            command.Handle();
            return Ok();
        }
    }
}
