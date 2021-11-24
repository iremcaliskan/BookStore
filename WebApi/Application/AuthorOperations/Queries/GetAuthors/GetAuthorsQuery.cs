using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x => x.Id).ToList();

            var vm = _mapper.Map<List<AuthorsViewModel>>(authors);
            return vm;
        }
    }
    public class AuthorsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}