using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record CreateTodoListCommand : IRequest<int>
    {
        public string Title { get; init; }
    }

    public class CreateTodoListHandler : IRequestHandler<CreateTodoListCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList();

            entity.Title = request.Title;

            _context.TodoList.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

    }
}