using Application.Common;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record DeleteTodoItemCommand(int id) : IRequest<Response<Unit>>;

    public class DeleteTodoItemHandler : IRequestHandler<DeleteTodoItemCommand, Response<Unit>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoItemHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response<Unit>> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.TodoList.SingleOrDefaultAsync(x => x.Id == request.id);

                if (entity == null)
                {
                    return null;
                }

                _context.TodoList.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Response<Unit>.Success(Unit.Value);

            }
            catch (Exception e)
            {
                return Response<Unit>.Failure("Error deleting todo item");
            }
        }

    }
}