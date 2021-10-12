using FluentValidation;
using System;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand> // Bir validasyon class'ı olduğu belirtildi, UpdateBookCommand'ı valide edecek
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
            RuleFor(x => x.Model.AuthorId).GreaterThan(0);
            RuleFor(x => x.Model.GenreId).GreaterThan(0);
            RuleFor(x => x.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(x => x.Model.PageCount).GreaterThan(0);
            RuleFor(x => x.Model.PublishDate.Date).LessThan(DateTime.Now.Date);
        }
    }
}