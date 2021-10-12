using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("No author found to be updated!");
            }

            var vm = _mapper.Map<Author>(Model);
            vm.FirstName = Model.FirstName != default ? Model.FirstName : author.FirstName;
            vm.LastName = Model.LastName != default ? Model.LastName : author.LastName;
            vm.Birthdate = Model.Birthdate != default ? Model.Birthdate : author.Birthdate;

            _context.Authors.Update(vm);
            _context.SaveChanges();
        }
    }
    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}