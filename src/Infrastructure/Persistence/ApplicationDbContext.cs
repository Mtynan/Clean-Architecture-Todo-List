using Application.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        public ApplicationDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<TodoList> TodoList { get; set; }
        public DbSet<TodoItem> TodoItem { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
