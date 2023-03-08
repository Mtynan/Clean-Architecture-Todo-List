using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.TodoListFeatures.Commands
{
    public record CreateTodoItemCommand : IRequest<Response>
    {
        public string Title { get; init; }
        public int ListId { get; set; }
    }

    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, Response>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(IApplicationDbContext), $"{nameof(IApplicationDbContext)} cannot be null");
        }

        public async Task<Response> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem();

            entity.Title = request.Title;
            entity.ListId = request.ListId;

            _context.TodoItem.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return new Response { Success = true };
        }

    }
}