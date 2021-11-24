using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(x => x.FirstName.ToLower() == Model.FirstName.ToLower() && x.LastName.ToLower() == Model.LastName.ToLower() && x.Birthdate.Date == Model.Birthdate.Date);
            if (author is not null)
            {
                throw new InvalidOperationException("The author is already in the system.");
            }

            var vm = _mapper.Map<Author>(Model);

            _context.Authors.Add(vm);
            _context.SaveChanges();
        }
    }
    public class CreateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}