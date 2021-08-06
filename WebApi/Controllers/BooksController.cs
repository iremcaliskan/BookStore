﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        //private static List<Book> BookList = new List<Book>()
        //{
        //     new Book
        //     {
        //         Title = "Book 1", GenreId = 1, PageCount = 300, PublishDate = new DateTime(2001, 06, 12)
        //     },
        //     new Book
        //     {
        //         Title = "Book 2", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2003, 07, 18)
        //     },
        //     new Book
        //     {
        //         Title = "Book 3", GenreId = 3, PageCount = 266, PublishDate = new DateTime(2010, 01, 01)
        //     },
        //     new Book
        //     {
        //         Title = "Book 4", GenreId = 3, PageCount = 377, PublishDate = new DateTime(2009, 07, 07)
        //     },
        //     new Book
        //     {
        //         Title = "Book 5", GenreId = 2, PageCount = 321, PublishDate = new DateTime(2010, 07, 07)
        //     },
        //     new Book
        //     {
        //         Title = "Book 6", GenreId = 3, PageCount = 116, PublishDate = new DateTime(2011, 07, 07)
        //     }
        //};

        // HTTP verb attributelarına(HttpGet, HttpPost, HttpPut, HttpDelete...) ek olarak, eylem imza düzeyi(action signature level) attributeları bulunur.
        // Metoda, eyleme, action'a HTTP fiil öznitelikleri uygulandığında, bu öznitelikler parametre listesinde kullanılır.
        // FromServicesAttribute, FromRouteAttribute, FromQueryAttribute, FromBodyAttribute, FromFormAttribute

        // .NET, güçlü bir Route özniteliği sağlar.
        // Bu, controller sınıfında üst düzey bir rota tanımlamak için kullanılabilir – bu, eylemlerin genişletilebileceği ortak bir rota bırakır.
        // Route özniteliğine bir "api/[controller]" şablonu verilir.
        // "[controller]", controller için yer tutucu görevi gören özel bir adlandırma kuralıdır.

        // HttpGet'e odakladığımızda, "{id}" şablon argümanını görebiliriz. Bu, HTTP Get yolunun "api/orders/1"e benzemesini sağlar.
        // FromRoute niteliği, çerçeveye bir "id" değeri için rotaya (URL) bakmasını ve bunu id argümanı olarak sağlamasını söyler.

        // [FromQuery(Name = "identifier")] int id
        // HttpGet("api/orders") // api/orders?identifier=7
        // FromQuery özniteliğinin "identifier" dizesine eşit olacak olan Name özelliğini atanmış.
        // Bu, çerçeveye sorgu dizesindeki adla eşleşen bir ad aramasını söyler.
        // Adın, eylemler parametresi olarak kullanılan ad "id" olduğu varsayılır.
        // Başka bir deyişle, "api/orders?id=17" olarak bir URL'miz varsa, çerçeve açıkça "identifier" adında bir sorgu dizesi aradığı için "id" değişkenimize 17 sayısını atamayacaktır.

        //[HttpGet]
        //public List<Book> GetBooks()
        //{
        //    var bookList = BookList.OrderBy(x => x.Id).ToList<Book>(); // ToList from IEnumerable to List
        //    return bookList;
        //}

        //[HttpGet("{id}")] // localhost:44392/api/Books/1
        //public Book GetById(int id)
        //{
        //    var book = BookList.Where(x => x.Id == id).SingleOrDefault(x => x.Id == id);
        //    return book;
        //}

        // QueryString gözden kaçarsa, GetAll yapan Endpoint ile karışabilir.
        //[HttpGet] // localhost:44392/api/Books?id=1
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        //[HttpGet("{id}")] // localhost:44392/api/Books/1
        //public Book GetById([FromRoute] int id)
        //{
        //    var book = BookList.Where(x => x.Id == id).SingleOrDefault(x => x.Id == id);
        //    return book;
        //}

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

        public BooksController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /* Entity Kavramı ve Action Metotları: */
        //[HttpGet] // localhost:44392/api/Books
        //public Book GetBooks()
        //{
        //    var book = _context.Books.ToList<Book>();
        //    return book;
        //} 

        //[HttpGet("{id}")] // localhost:44392/api/Books/1
        //public Book GetById([FromRoute] int id)
        //{
        //    var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
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

        /* Entity, View Model ve Dto Kavramları */
        //[HttpGet]
        //public IActionResult GetBooks() // localhost:44392/api/Books
        //{
        //    GetBooksQuery query = new GetBooksQuery(_context);
        //    var result = query.Handle();
        //    return Ok(result);
        //}

        //[HttpGet("{id}")] // localhost:44392/api/Books/1
        //public IActionResult GetById([FromRoute] int id)
        //{
        //    BookDetailViewModel result;
        //    try
        //    {
        //        GetBookDetailQuery query = new GetBookDetailQuery(_context);
        //        query.BookId = id;
        //        result = query.Handle();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok(result);
        //}

        //[HttpPost] // Body'den Resource gönderme
        //public IActionResult AddBook([FromBody] CreateBookModel newBook)
        //{
        //    CreateBookCommand command = new CreateBookCommand(_context);
        //    try
        //    {
        //        command.Model = newBook;
        //        command.Handle(); // İçerisinde Exception içeriyor try-catch bloğu gerekli
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message); // Requestler IActionResult ister
        //    }
        //    return Ok(); // Requestler IActionResult ister
        //}

        //[HttpPut("{id}")] // Route'dan id ile kitabı alma + güncel değerleri gönderme
        //public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        //{
        //    try
        //    {
        //        UpdateBookCommand command = new UpdateBookCommand(_context);
        //        command.BookId = id;
        //        command.Model = updatedBook;
        //        command.Handle(); // İçerisinde Exception içeriyor try-catch bloğu gerekli
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message); // Requestler IActionResult ister
        //    }
        //    return Ok(); // Requestler IActionResult ister
        //}

        //[HttpDelete("{id}")] // Route'dan id ile kitabı alma
        //public IActionResult DeleteBook(int id)
        //{
        //    try
        //    {
        //        DeleteBookCommand command = new DeleteBookCommand(_context);
        //        command.BookId = id;
        //        command.Handle();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok();
        //}

        /* Entity, View Model ve Dto Kavramları + AutoMapper */
        [HttpGet]
        public IActionResult GetBooks() // localhost:44392/api/Books
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] // localhost:44392/api/Books/1
        public IActionResult GetById([FromRoute] int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")] // Route'dan id ile kitabı alma + güncel değerleri gönderme
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

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
    }
}