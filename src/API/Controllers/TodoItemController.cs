using Application;
using Application.Common;
using Application.TodoListFeatures.Commands;
using Application.TodoListFeatures.Queries;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodoItemController : BaseApiController
    {
        private readonly IMyDependency _myDependency;
        public TodoItemController(ApplicationDbContext context, IMyDependency myDependency)
        {
            _myDependency = myDependency;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodoItems()
        {
            return await Mediator.Send(new GetTodoItemQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateTodoItem(CreateTodoItemCommand command)
        {
            return await Mediator.Send(new CreateTodoItemCommand { Title = command.Title, ListId = command.ListId });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateTodoItem(UpdateTodoItemCommand command)
        {
            return await Mediator.Send(new UpdateTodoItemCommand { Title = command.Title, ListId = command.ListId });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteTodoItem(DeleteTodoItemCommand command)
        {
            return await Mediator.Send(new DeleteTodoItemCommand(command.id));
        }
    }
}
