using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoList { get; }
        DbSet<TodoItem> TodoItem { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}