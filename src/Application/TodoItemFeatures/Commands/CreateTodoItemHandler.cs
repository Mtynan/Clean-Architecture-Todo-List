using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record CreateTodoItemCommand : IRequest<Response<TodoItem>>
    {
        public string Title { get; init; }
        public int ListId { get; set; }
    }

    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, Response<TodoItem>>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response<TodoItem>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem();

            entity.Title = request.Title;
            entity.ListId = request.ListId;
            entity.Created = DateTime.Now;

            _context.TodoItem.Add(entity);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Response<TodoItem>.Failure("Failed to create Todo Item");

            return Response<TodoItem>.Success(entity);
        }

    }
}