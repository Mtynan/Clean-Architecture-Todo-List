using Application.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoListFeatures.Commands
{
    public record UpdateTodoItemCommand : IRequest<Response<Unit>>
    {
        public string Title { get; init; }
        public int ListId { get; set; }
    }

    public class UpdateTodoItemHandler : IRequestHandler<UpdateTodoItemCommand, Response<Unit>>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response<Unit>> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItem.FirstOrDefaultAsync(x => x.ListId == request.ListId);

            if (entity == null) return null;

            entity.Title = request.Title;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Response<Unit>.Failure("Failed to update Todo Item");

            return Response<Unit>.Success(Unit.Value);
        }

    }
}