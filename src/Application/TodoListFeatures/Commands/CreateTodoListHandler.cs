using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record CreateTodoListCommand : IRequest<Response>
    {
        public string Title { get; init; }
    }

    public class CreateTodoListHandler : IRequestHandler<CreateTodoListCommand, Response>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList();

            entity.Title = request.Title;

            _context.TodoList.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return new Response { Success = true };
        }

    }
}