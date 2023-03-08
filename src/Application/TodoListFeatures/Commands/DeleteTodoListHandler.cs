using Application.Common;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record DeleteTodoListCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }

    public class DeleteTodoListHandler : IRequestHandler<DeleteTodoListCommand, Response>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoListHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
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

                return new Response { Success = true };

            }
            catch (Exception e)
            {
                return new Response { Success = false };
            }

        }

    }
}