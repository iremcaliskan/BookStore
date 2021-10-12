using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand> // Bir validasyon class'ı olduğu belirtildi, DeleteBookCommand'ı valide edecek
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}