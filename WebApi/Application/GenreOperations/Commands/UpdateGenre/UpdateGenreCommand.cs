using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;   
        public UpdateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("No genre found to be updated!");
            }

            var vm = _mapper.Map<Genre>(genre);
            vm.Name = Model.Name != default ? Model.Name : genre.Name; // null, empty string değilse

            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    { // Güncellenebilir değerler
        public string Name { get; set; }
    }
}