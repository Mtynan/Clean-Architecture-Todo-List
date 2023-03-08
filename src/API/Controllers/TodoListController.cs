using Application;
using Application.Common;
using Application.TodoListFeatures.Commands;
using Application.TodoListFeatures.Queries;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodoListController : BaseApiController
    {
        private readonly IMyDependency _myDependency;
        public TodoListController(ApplicationDbContext context, IMyDependency myDependency)
        {
            _myDependency = myDependency;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoList>>> GetTodoLists()
        {
            return await Mediator.Send(new GetTodosQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateTodoList(CreateTodoListCommand command)
        {
            return await Mediator.Send(new CreateTodoListCommand { Title = command.Title });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateTodoList(UpdateTodoListCommand command)
        {
            return await Mediator.Send(new UpdateTodoListCommand { Title = command.Title, Id = command.Id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteTodoList(DeleteTodoListCommand command)
        {
            return await Mediator.Send(new DeleteTodoListCommand { Id = command.Id });
        }
    }
}
