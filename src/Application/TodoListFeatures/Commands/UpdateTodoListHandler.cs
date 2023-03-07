using Application.Common;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record UpdateTodoListCommand : IRequest<int>
    {
        public string Title { get; init; }
        public int Id { get; set; }
    }

    public class UpdateTodoListHandler : IRequestHandler<UpdateTodoListCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<int> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.TodoList.FirstOrDefault();

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

    }
}