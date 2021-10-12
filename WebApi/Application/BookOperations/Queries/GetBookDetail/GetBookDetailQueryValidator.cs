using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}