using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("No book found to be updated!");
            }

            //book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId; // 0 değilse? Default'u değişmiş mi? Değişmişse Model değişmemişse book GenreId
            //book.Title = Model.Title != default ? Model.Title : book.Title; // null, empty string değilse

            var vm = _mapper.Map<Book>(book);
            vm.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
            vm.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId; // 0 değilse? Default'u değişmişse Model, değişmemişse book GenreId
            vm.Title = Model.Title != default ? Model.Title : book.Title; // String default null
            vm.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount; // Int default 0
            vm.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate; // Datetime default 0001

            _context.SaveChanges();
        }
    }

    public class UpdateBookModel
    { // Güncellenebilir değerler
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}