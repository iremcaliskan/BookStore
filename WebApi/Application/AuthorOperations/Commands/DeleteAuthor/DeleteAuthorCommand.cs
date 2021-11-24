using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }

        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.AuthorId == AuthorId);
            if (book is not null)
            {
                throw new InvalidOperationException("Author has a published book in the system!");
            }

            var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("No author found to be deleted!");
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}