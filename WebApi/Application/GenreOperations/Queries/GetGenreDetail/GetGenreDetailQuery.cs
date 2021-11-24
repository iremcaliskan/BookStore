using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.Where(x => x.Id == GenreId).FirstOrDefault();
            if (genre is null)
            {
                throw new InvalidOperationException("Genre is not found!");
            }

            //GenreDetailViewModel vm = new GenreDetailViewModel()
            //{
            //    Id = genre.Id,
            //    Name = genre.Name
            //};

            // Gelen Genre'yi GenreDetailViewModel olarak maple ve döndür
            var vm = _mapper.Map<GenreDetailViewModel>(genre);
            return vm;
        }
    }

    public class GenreDetailViewModel
    { // UI'a(View'a) dönecek bilgiler için ViewModel kullanılımalıdır.
        public int Id { get; set; }
        public string Name { get; set; }
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