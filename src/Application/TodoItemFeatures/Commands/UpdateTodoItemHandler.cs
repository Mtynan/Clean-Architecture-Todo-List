using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoListFeatures.Commands
{
    public record UpdateTodoItemCommand : IRequest<Response>
    {
        public string Title { get; init; }
        public int ListId { get; set; }
    }

    public class UpdateTodoItemHandler : IRequestHandler<UpdateTodoItemCommand, Response>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItem.FirstOrDefaultAsync(x => x.ListId == request.ListId);

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return new Response { Success = true };
        }

    }
}