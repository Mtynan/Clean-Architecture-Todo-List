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
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            return HandleResult(await Mediator.Send(new GetTodoItemQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(CreateTodoItemCommand command)
        {
            return HandleResult(await Mediator.Send(new CreateTodoItemCommand { Title = command.Title, ListId = command.ListId }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(UpdateTodoItemCommand command)
        {
            return HandleResult(await Mediator.Send(new UpdateTodoItemCommand { Title = command.Title, ListId = command.ListId }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(DeleteTodoItemCommand command)
        {
            return HandleResult(await Mediator.Send(new DeleteTodoItemCommand(command.id)));
        }
    }
}
