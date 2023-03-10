using Application.Common;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record DeleteTodoListCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }

    public class DeleteTodoListHandler : IRequestHandler<DeleteTodoListCommand, Response<Unit>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoListHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response<Unit>> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.TodoList.SingleOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                {
                    throw new Exception("Unable to find TodoList to Delete");
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

