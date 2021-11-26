using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand> // Bir validasyon class'ı olduğu belirtildi, UpdateGenreCommand'ı valide edecek
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}