using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")] // BooksController -- api/[controller] ya da BookController -- api/[controller]s
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BooksController(BookStoreDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public List<Book> GetBooks()
        //{
        //    var bookList = BookList.OrderBy(x => x.Id).ToList<Book>(); // ToList from IEnumerable to List
        //    return bookList;
        //}

        [HttpGet]
        public IActionResult GetBooks() // localhost:44392/api/Books
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] // localhost:44392/api/Books/1
        public IActionResult GetById([FromRoute] int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        //[HttpGet("{id}")] // localhost:44392/api/Books/1
        //public Book GetById([FromRoute] int id)
        //{
        //    var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
        //    return book;
        //}

        //[HttpGet("{id}")] // localhost:44392/api/Books/1
        //public Book GetById([FromRoute] int id)
        //{
        //    var book = BookList.Where(x => x.Id == id).SingleOrDefault(x => x.Id == id);
        //    return book;
        //}

        // QueryString gözden kaçarsa, GetAll yapan Endpoint ile karışabilir.
        //[HttpGet] // localhost:44392/api/Books?id=1
        //public Book Get([FromQuery]int id)
        //{
        //    var book = BookList.SingleOrDefault(x => x.Id == id);
        //    return book;
        //}

        //[HttpPost] // Body'den Resource gönderme
        //public IActionResult AddBook([FromBody] Book newBook)
        //{
        //    var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
        //    if (book is not null)
        //    {
        //        return BadRequest(); // Requestler IActionResult ister
        //    }

        //    _context.Books.Add(newBook);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        [HttpPost] // Body'den Resource gönderme
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle(); // İçerisinde Exception içeriyor try-catch bloğu gerekli
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Requestler IActionResult ister
            }
            return Ok(); // Requestler IActionResult ister
        }

        //[HttpPost] // Body'den Resource gönderme
        //public IActionResult AddBook([FromBody] Book newBook)
        //{
        //    var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
        //    if (book is not null)
        //    {
        //        return BadRequest(); // Requestler IActionResult ister
        //    }

        //    BookList.Add(newBook);
        //    return Ok();
        //}

        [HttpPut("{id}")] // Route'dan id ile kitabı alma + güncel değerleri gönderme
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle(); // İçerisinde Exception içeriyor try-catch bloğu gerekli
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Requestler IActionResult ister
            }
            return Ok(); // Requestler IActionResult ister
        }

        //[HttpPut("{id}")] // Route'dan id ile kitabı alma + güncel değerleri gönderme
        //public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        //{
        //    var book = _context.Books.SingleOrDefault(x => x.Id == id);
        //    if (book is null)
        //    {
        //        return BadRequest();
        //    }

        //    book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId; // 0 değilse? Default'u değişmiş mi? Değişmişse updatedBook değişmemişse book GenreId
        //    book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        //    book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title; // null, empty string değilse
        //    book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate; // Default olarak sistem saatini girer Swagger,  her seferinde günceller dışarıdan gelirse 001 girer güncelleme yapmaz

        //    _context.SaveChanges();
        //    return Ok();
        //}

        //[HttpPut("{id}")] // Route'dan id ile kitabı alma + güncel değerleri gönderme
        //public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        //{
        //    var book = BookList.SingleOrDefault(x => x.Id == id);
        //    if (book is null)
        //    {
        //        return BadRequest();
        //    }

        //    book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId; // 0 değilse? Default'u değişmiş mi? Değişmişse updatedBook değişmemişse book GenreId
        //    book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        //    book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title; // null, empty string değilse
        //    book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate; // Default olarak sistem saatini girer Swagger,  her seferinde günceller dışarıdan gelirse 001 girer güncelleme yapmaz
        //    return Ok();
        //}

        [HttpDelete("{id}")] // Route'dan id ile kitabı alma
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        //[HttpDelete("{id}")] // Route'dan id ile kitabı alma
        //public IActionResult DeleteBook(int id)
        //{
        //    var book = _context.Books.SingleOrDefault(x => x.Id == id);
        //    if (book is null)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Books.Remove(book);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        //[HttpDelete("{id}")] // Route'dan id ile kitabı alma
        //public IActionResult DeleteBook(int id)
        //{
        //    var book = BookList.SingleOrDefault(x => x.Id == id);
        //    if (book is null)
        //    {
        //        return BadRequest();
        //    }

        //    BookList.Remove(book);
        //    return Ok();
        //}
    }
}