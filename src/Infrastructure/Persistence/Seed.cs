using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            if (context.TodoList.Any()) return;
            var list = new List<TodoList>
            {

                new TodoList
                {
                    Created = DateTime.Now,
                    Id = 1,
                    Title = "test",

                }
            };
            await context.TodoList.AddRangeAsync(list);
            await context.SaveChangesAsync();

        }
    }
}
