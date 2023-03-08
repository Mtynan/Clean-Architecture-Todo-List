using Application.Common;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record DeleteTodoItemCommand(int id) : IRequest<Response>;

    public class DeleteTodoItemHandler : IRequestHandler<DeleteTodoItemCommand, Response>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoItemHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.TodoList.SingleOrDefaultAsync(x => x.Id == request.id);

                if (entity == null)
                {
                    throw new Exception("Unable to find TodoItem to Delete");
                }

                _context.TodoList.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response { Success = true };

            }
            catch (Exception e)
            {
                return new Response { Success = false };
            }
        }

    }
}