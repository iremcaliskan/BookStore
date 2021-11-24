using FluentValidation;
using System;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.FirstName).NotEmpty().NotEmpty();
            RuleFor(x => x.Model.LastName).NotEmpty().NotEmpty();
            RuleFor(x => x.Model.Birthdate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}