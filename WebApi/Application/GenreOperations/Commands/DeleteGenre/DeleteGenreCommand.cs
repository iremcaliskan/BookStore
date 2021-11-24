using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _context;
        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("No genre found to be deleted!");
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}