using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoListFeatures.Commands
{
    public record UpdateTodoListCommand : IRequest<Response>
    {
        public string Title { get; init; }
        public int Id { get; set; }
    }

    public class UpdateTodoListHandler : IRequestHandler<UpdateTodoListCommand, Response>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoList.FirstOrDefaultAsync();

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return new Response { Success = true };
        }

    }
}