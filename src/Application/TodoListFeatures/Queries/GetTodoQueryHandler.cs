using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoListFeatures.Queries
{

    public record GetTodosQuery : IRequest<List<TodoList>>;

    public class GetTodoQueryHandler : IRequestHandler<GetTodosQuery, List<TodoList>>
    {

        private readonly IApplicationDbContext _context;

        public GetTodoQueryHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<List<TodoList>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return await _context.TodoList.ToListAsync();
        }
    }
}
