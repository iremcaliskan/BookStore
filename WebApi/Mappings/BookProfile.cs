using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.Common;

namespace WebApi.Mappings
{
    public class BookProfile : Profile // Profile sınıfından kalıtım alarak AutoMapper tarafından config dosyası olarak görülmesi sağlanır
    {
        public BookProfile()
        {
            // CreateMap<source, target>(); // Source'dan Target'a dönüşüm yapabilsin
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(destination => destination.Genre, option => option.MapFrom(source => ((GenreEnum)source.GenreId).ToString()));
            // BookDetailViewModel'ın Genre'si Book'un GenreId'sinin string versiyonuna maplenmiş halidir.

            CreateMap<Book, BooksViewModel>().ForMember(destination => destination.Genre, option => option.MapFrom(source => ((GenreEnum)source.GenreId).ToString()));
            // BooksViewModel'ın Genre'si Book'un GenreId'sinin string versiyonuna maplenmiş halidir.

            CreateMap<UpdateBookModel, Book>();
        }
    }
}