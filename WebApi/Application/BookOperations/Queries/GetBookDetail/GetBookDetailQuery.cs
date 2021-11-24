using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Include(x => x.Author).Include(x => x.Genre).Where(x => x.Id == BookId).FirstOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Book is not found!");
            }

            //BookDetailViewModel vm = new BookDetailViewModel()
            //{
            //    Title = book.Title,
            //    Genre = ((GenreEnum)book.GenreId).ToString(),
            //    PageCount = book.PageCount,
            //    PublishDate = book.PublishDate.ToString("dddd/MM/yyyy")
            //};

            // Gelen Book'u BookDetailViewModel olarak maple ve döndür
            var vm = _mapper.Map<BookDetailViewModel>(book);
            return vm;
        }
    }

    public class BookDetailViewModel
    { // UI'a(View'a) dönecek bilgiler için ViewModel kullanılımalıdır.
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
/* ViewModel
 * View model kullanıcıya gösterilecek olan verinin modelidir diyebiliriz.
 * Genelde UI bazında tanımlanır. Yani bir View'ın bir ViewModel'i olması beklenir. Birden fazla olmamalılıdır.
 * Bir ViewModel de birden fazla View'da kullanılmamalıdır. Bir işleve özel olmalıdır.
 * Çünkü ViewModel'lerin kullanılma motivativasyonu gereksiz bilginin Client'a indirilmemesidir.
 * Yani bir View'da kullanılmayacak olan veri API'dan geri döndürülmemelir.
 * Bu veri güvenliği açığına neden olur!
 * 
 * Disiplinli bir şekilde en küçük bir ekran için bile varolan ViewModel kullanılmadan yeni bir ViewModel yapılarak kullanılmalıdır.
 * Böyle yapılmazsa hem performans hemde güvenlik sorunu oluşturur. Koca bir objeyi dönmenin gereği yoktur.
 * 
 * Bir diğer kritik nokta ise entity'e veri map'lemek(post, put) için kullanılan model ile UI'a veri dönmek için kullanılan modelin 
 * aynı olmasıdır. Bu kullanımda fazladan verinin expose edilmesine yani client'a indirilmesine neden olur.
 * Bu yaklaşımda veri güvenliği sorunlarına neden olacağı için kaçınılmalıdır.
 * 
 * ViewModelleri son kullanıcıya veri dönmek dışında kullanmamamız gerekir. 
 * Son kullanıcı request'ine döndürülecek verileri maplemek için kullanılmalıdır.
 */