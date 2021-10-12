using AutoMapper;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            // CreateMap<source, target>(); // Source'dan Target'a dönüşüm yapabilsin
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<UpdateGenreModel, Genre>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Genre, GenresViewModel>();
        }
    }
}