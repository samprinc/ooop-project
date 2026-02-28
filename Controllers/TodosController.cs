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
        new TodoItem { Id = 1, Title = "Learn ASP.NET", IsCompleted = false, Description = "Go through the official documentation and build a sample project." ,Email="wishenganatal@gmail.com"},
        new TodoItem { Id = 2, Title = "Drink Water", IsCompleted = true }
    };

    // GET: api/todos
    //retrieves data from the server.
    [HttpGet("{id}")]
    public IActionResult GetTodos(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            return NotFound($"Todo item with ID {id} not found.");
        }
        return Ok(todo);
    }

    // GET: api/todos
    //retrieves all todos from the server.
    [HttpGet]
    public IActionResult GetAllTodos()
    {
        return Ok(_todos);
    }

    // POST: api/todos
    // creates data and adds something new
    [HttpPost]
    public IActionResult AddTodo(TodoItem newItem)
    {
        newItem.Id = _todos.Count + 1;
        _todos.Add(newItem);
        return Ok(newItem);
    }
    // PUT: api/todos/{id}
    // updates an existing todo item
    [HttpPut("{id}")]
    public IActionResult UpdateTodo(int id, TodoItem updatedItem)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            return NotFound($"Todo item with ID {id} not found.");
        }

        todo.Title = updatedItem.Title;
        todo.Description = updatedItem.Description;
        todo.IsCompleted = updatedItem.IsCompleted;
        todo.Email = updatedItem.Email;

        return Ok(todo);
    }
}
