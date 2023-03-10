using Application.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoListFeatures.Queries
{

    public record GetTodoItemQuery : IRequest<Response<List<TodoItem>>>
    {
        public int ListId { get; set; }
    }

    public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, Response<List<TodoItem>>>
    {

        private readonly IApplicationDbContext _context;

        public GetTodoItemQueryHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response<List<TodoItem>>> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
        {
            return Response<List<TodoItem>>.Success(await _context.TodoItem.Where(x => x.ListId == request.ListId).ToListAsync());
        }

    }
}
