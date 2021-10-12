using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Entities;

namespace WebApi.Mappings
{
    public class BookProfile : Profile // Profile sınıfından kalıtım alarak AutoMapper tarafından config dosyası olarak görülmesi sağlanır
    {
        public BookProfile()
        {
            // CreateMap<source, target>(); // Source'dan Target'a dönüşüm yapabilsin
            CreateMap<CreateBookModel, Book>();
            //CreateMap<Book, BookDetailViewModel>().ForMember(destination => destination.Genre, option => option.MapFrom(source => ((GenreEnum)source.GenreId).ToString()));
            // BookDetailViewModel'ın Genre'si Book'un GenreId'sinin string versiyonuna maplenmiş halidir.
            
            //CreateMap<Book, BooksViewModel>().ForMember(destination => destination.Genre, option => option.MapFrom(source => ((GenreEnum)source.GenreId).ToString()));
            // BooksViewModel'ın Genre'si Book'un GenreId'sinin string versiyonuna maplenmiş halidir.
            CreateMap<UpdateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(destination => destination.Genre, option => option.MapFrom(source => source.Genre.Name))
                .ForMember(destination => destination.AuthorFullName, option => option.MapFrom(source => $"{source.Author.FirstName} {source.Author.LastName}"));
            CreateMap<Book, BooksViewModel>()
                .ForMember(destination => destination.Genre, option => option.MapFrom(source => source.Genre.Name))
                .ForMember(destination => destination.AuthorFullName, option => option.MapFrom(source => $"{source.Author.FirstName} {source.Author.LastName}"));
        }
    }
}