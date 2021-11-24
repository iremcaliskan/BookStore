using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());
            if (genre is not null)
            {
                throw new InvalidOperationException("The genre is already in the system.");
            }

            // Model üzerinden tek tek maplemeyi ortadan kaldıran Automapper'dır. Bu fazla bir kod yazımıdır.
            //genre = new Genre
            //{ // Gelen modelin özelliklerini kitaba eşitleyip db'ye ekleme yapılacak
            //    Name = Model.Name
            //};
            genre = _mapper.Map<Genre>(Model); // Model ile gelen veriyi Genre objesine dönüştür.

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    { // Post ve Put işlemleri için, aynı gözükse bile ek sınıf yazılmalıdır, ileride işler değişir.
        public string Name { get; set; }
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