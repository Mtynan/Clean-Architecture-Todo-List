using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoListFeatures.Commands
{
    public record UpdateTodoListCommand : IRequest<Response<Unit>>
    {
        public string Title { get; init; }
        public int Id { get; set; }
    }

    public class UpdateTodoListHandler : IRequestHandler<UpdateTodoListCommand, Response<Unit>>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response<Unit>> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoList.FirstOrDefaultAsync();

            if (entity == null) return null;

            entity.Title = request.Title;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Response<Unit>.Failure("Failed to update Todo List");

            return Response<Unit>.Success(Unit.Value);
        }

    }
}


