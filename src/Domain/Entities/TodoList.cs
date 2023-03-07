using Domain.Common;

namespace Domain.Entities
{
    public class TodoList : EntityBase
    {
        public string Title { get; set; }
        public List<TodoItem> Items { get; set; } = new List<TodoItem>();
    }
}
