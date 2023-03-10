using Application.TodoListFeatures.Commands;
using Application.TodoListFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodoListController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetTodoLists()
        {
            return HandleResult(await Mediator.Send(new GetTodosQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoList(CreateTodoListCommand command)
        {
            return HandleResult(await Mediator.Send(new CreateTodoListCommand { Title = command.Title }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoList(UpdateTodoListCommand command)
        {
            return HandleResult(await Mediator.Send(new UpdateTodoListCommand { Title = command.Title, Id = command.Id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(DeleteTodoListCommand command)
        {
            return HandleResult(await Mediator.Send(new DeleteTodoListCommand { Id = command.Id }));
        }
    }
}
