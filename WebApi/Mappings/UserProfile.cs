using AutoMapper;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Entities;

namespace WebApi.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserModel, User>();
        }
    }
}