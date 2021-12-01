using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Model.Email).NotEmpty().NotNull();
            RuleFor(x => x.Model.Password).NotEmpty().NotNull();
        }
    }
}