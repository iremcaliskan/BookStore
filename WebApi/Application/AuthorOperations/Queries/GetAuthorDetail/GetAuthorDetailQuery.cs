using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Author is not found!");
            }

            var vm = _mapper.Map<AuthorDetailViewModel>(author);
            return vm;
        }
    }
    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}