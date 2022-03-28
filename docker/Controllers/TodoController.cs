using Microsoft.AspNetCore.Mvc;

namespace MicroService.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoStore _store;
    public TodoController(TodoStore store) {
        _store = store;
    }

    [HttpGet(Name = "GetTodos")]
    public TodoResponse GetAll() => new TodoResponse {
        Message = "Retrieved all todos",
        Data = _store.GetAll()
    };

    [HttpGet("{id:int}", Name = "GetTodo")]
    public TodoResponse Get(int id) {
        var todo = _store.Get(id);

        if (todo is null) {
            return new TodoResponse {
                Message = "Todo not found",
                Status = false
            };
        }

        return new TodoResponse {
            Message = $"Retrieve todo with id {id}",
            Data = todo
        };
    }

    [HttpPost(Name = "AddTodo")]
    public TodoResponse Add(Todo item) {
        var todo = _store.Add(item);

        return new TodoResponse {
            Message = "Todo added succesfully",
            Data = todo
        };
    }

    [HttpPut("{id:int}", Name = "EditTodo")]
    public TodoResponse Edit(int id, Todo item) {
        var todo = _store.Update(id, item);
        if (!todo) {
            return new TodoResponse {
                Message = "Todo not found",
                Status = false
            };
        }

        return new TodoResponse { Message = "Todo updated successfully" };
    }

    [HttpDelete("{id:int}", Name = "DeleteTodo")]
    public TodoResponse Delete(int id) {
        if (!_store.Remove(id)) {
            return new TodoResponse {
                Message = "Todo not found",
                Status = false
            };
        }

        return new TodoResponse { Message = "Todo deleted successfully" };
    }
}
