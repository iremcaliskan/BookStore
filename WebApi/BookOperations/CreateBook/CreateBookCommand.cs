using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; } // Model dışarıdan gelecek
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("The book is already in the system.");
            }

            // Model üzerinden tek tek maplemeyi ortadan kaldıran Automapper'dır. Bu fazla bir kod yazımıdır.
            //book = new Book
            //{ // Gelen modelin özelliklerini kitaba eşitleyip db'ye ekleme yapılacak
            //    Title = Model.Title,
            //    GenreId = Model.GenreId,
            //    PageCount = Model.PageCount,
            //    PublishDate = Model.PublishDate
            //};
            book = _mapper.Map<Book>(Model); // Model ile gelen veriyi Book objesine dönüştür.

            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }

    public class CreateBookModel
    { // Post ve Put işlemleri için, aynı gözükse bile ek sınıf yazılmalıdır, ileride işler değişir.
        public string Title { get; set; }
        public int GenreId { get; set; } // Türü
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
/* ViewModel vs Model
 * Diğer kritik nokta, Entity'e veri map'lemek(post, put) için kullanılan Model ile UI'a veri dönmek için kullanılan modelin aynı olmasıdır.
 * Bu şekilde kullanım fazladan verinin expose edilmesine yani Client'a indirilmesine neden olur.
 * Bu yaklaşımda veri güvenliği sorunlarına neden olacağı için kaçınılmalıdır.
 * 
 * ViewModeller son kullanıcıya veri dönmek dışında kullanılmamalıdır.
 * Son kullanıcı Request'ine dönecek verileri maplemek için kullanılmalıdır.
 */