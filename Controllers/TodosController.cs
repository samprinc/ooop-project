using Microsoft.AspNetCore.Mvc;
using SimpleTodoAPI.Models;

namespace SimpleTodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    // The Static List (Our Temporary Database)
    private static List<TodoItem> _todos = new List<TodoItem>
    {
        new TodoItem { Id = 1, Title = "Learn ASP.NET", IsCompleted = false },
        new TodoItem { Id = 2, Title = "Drink Water", IsCompleted = true }
    };

    // GET: api/todos
    [HttpGet]
    public IActionResult GetTodos()
    {
        return Ok(_todos);
    }

    // POST: api/todos
    [HttpPost]
    public IActionResult AddTodo(TodoItem newItem)
    {
        newItem.Id = _todos.Count + 1;
        _todos.Add(newItem);
        return Ok(newItem);
    }
}