using Application.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoListFeatures.Queries
{

    public record GetTodosQuery : IRequest<Response<List<TodoList>>>
    {
        public int Id { get; set; }
    }

    public class GetTodoQueryHandler : IRequestHandler<GetTodosQuery, Response<List<TodoList>>>
    {

        private readonly IApplicationDbContext _context;

        public GetTodoQueryHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response<List<TodoList>>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return Response<List<TodoList>>.Success(await _context.TodoList.Where(x => x.Id == request.Id).ToListAsync());

        }
    }
}
