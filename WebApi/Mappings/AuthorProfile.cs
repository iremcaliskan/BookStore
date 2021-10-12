using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Entities;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;

namespace WebApi.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>();
        }
    }
}