using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand> // Bir validasyon class'ı olduğu belirtildi, CreateGenreCommand'ı valide edecek
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}