using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record CreateTodoListCommand : IRequest<Response<TodoList>>
    {
        public string Title { get; init; }
    }

    public class CreateTodoListHandler : IRequestHandler<CreateTodoListCommand, Response<TodoList>>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response<TodoList>> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList();

            entity.Title = request.Title;
            entity.Created = DateTime.Now;

            _context.TodoList.Add(entity);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Response<TodoList>.Failure("Failed to create Todo List");

            return Response<TodoList>.Success(entity);
        }

    }
}