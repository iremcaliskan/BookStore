using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand> // Bir validasyon class'ı olduğu belirtildi, DeleteGenreCommand'ı valide edecek
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);
        }
    }
}