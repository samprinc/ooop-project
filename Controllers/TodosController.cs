using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using SimpleTodoAPI.Models;
using SimpleTodoAPI.Data; 

namespace SimpleTodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    // This variable connects our Controller to the Database
    private readonly AppDbContext _context;

    // This constructor sets up the database connection automatically
    public TodosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/todos/{id} - Finds one specific task by its ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodo(int id)
    {
        // We look through the database for the matching ID
        var todo = await _context.TodoItems.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound($"Todo item with ID {id} not found.");
        }
        return Ok(todo);
    }

    // GET: api/todos - Gets the full list of tasks from the database
    [HttpGet]
    public async Task<IActionResult> GetAllTodos()
    {
        // Fetches every task saved in the SQL table
        var todos = await _context.TodoItems.ToListAsync();
        return Ok(todos);
    }

    // POST: api/todos - Adds a new task to the database
    [HttpPost]
    public async Task<IActionResult> AddTodo(TodoItem newItem)
    {
        // We add the new task to the list...
        _context.TodoItems.Add(newItem); 
        
        // ...and then we save it permanently to the database
        await _context.SaveChangesAsync(); 
        
        return Ok(newItem);
    }

    // PUT: api/todos/{id} - Updates an existing task
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, TodoItem updatedItem)
    {
        // First, find the task we want to change
        var todo = await _context.TodoItems.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound($"Todo item with ID {id} not found.");
        }

        // Apply the new changes to the task
        todo.Title = updatedItem.Title;
        todo.Description = updatedItem.Description;
        todo.IsCompleted = updatedItem.IsCompleted;
        todo.Email = updatedItem.Email;

        // Save the changes back to the database
        await _context.SaveChangesAsync();

        return Ok(todo);
    }

    // DELETE: api/todos/{id} - Removes a task permanently
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null)
        {
            return NotFound($"Todo item with ID {id} not found.");
        }

        // Remove the item and update the database
        _context.TodoItems.Remove(todo); 
        await _context.SaveChangesAsync(); 

        return NoContent(); 
    }
}