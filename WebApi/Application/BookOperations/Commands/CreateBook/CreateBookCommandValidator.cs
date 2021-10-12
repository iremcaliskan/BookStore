using FluentValidation;
using System;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand> // Bir validasyon class'ı olduğu belirtildi, CreateBookCommand'ı valide edecek
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Model.GenreId).GreaterThan(0);
            RuleFor(x => x.Model.AuthorId).GreaterThan(0);
            RuleFor(x => x.Model.PageCount).GreaterThan(0);
            RuleFor(x => x.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(x => x.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}