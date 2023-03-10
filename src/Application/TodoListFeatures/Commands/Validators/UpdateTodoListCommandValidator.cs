using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.TodoListFeatures.Commands.Validators
{
    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        public UpdateTodoListCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}