using FluentValidation;

namespace Application.TodoListFeatures.Commands
{
    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        public CreateTodoListCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}