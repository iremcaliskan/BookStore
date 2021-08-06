using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("No book found to be updated!");
            }

            //book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId; // 0 değilse? Default'u değişmiş mi? Değişmişse Model değişmemişse book GenreId
            //book.Title = Model.Title != default ? Model.Title : book.Title; // null, empty string değilse

            var vm = _mapper.Map<Book>(book);
            vm.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId; // 0 değilse? Default'u değişmiş mi? Değişmişse Model değişmemişse book GenreId
            vm.Title = Model.Title != default ? Model.Title : book.Title; // null, empty string değilse

            _context.SaveChanges();
        }
    }

    public class UpdateBookModel
    { // Güncellenebilir değerler
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}