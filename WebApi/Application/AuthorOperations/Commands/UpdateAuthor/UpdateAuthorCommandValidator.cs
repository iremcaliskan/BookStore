﻿using FluentValidation;
using System;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.Model.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Model.Birthdate).NotEmpty().LessThan(DateTime.Now);
        }
    }
}