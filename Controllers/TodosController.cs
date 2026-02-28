using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using SimpleTodoAPI.Models;
using SimpleTodoAPI.Data; 

namespace SimpleTodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    // This connects our code to the actual database
    private readonly AppDbContext _context;

    // This part makes sure the database connection is ready to use
    public TodosController(AppDbContext context)
    {
        _context = context;
    }

    // GET ALL: Shows the full list of tasks from the database
    [HttpGet]
    public async Task<IActionResult> GetAllTodos()
    {
        var todos = await _context.Todos.ToListAsync();
        return Ok(todos);
    }

    // GET ONE: Finds one specific task using its ID number
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound("Sorry, we couldn't find that task.");
        }
        return Ok(todo);
    }

    // POST: Adds a brand new task to the database
    [HttpPost]
    public async Task<IActionResult> AddTodo(TodoItem newItem)
    {
        _context.Todos.Add(newItem); 
        await _context.SaveChangesAsync(); // This line saves it permanently
        return Ok(newItem);
    }

    // PUT: Updates a task that is already in the database
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, TodoItem updatedItem)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound("Task not found.");
        }

        // Update the task details
        todo.Title = updatedItem.Title;
        todo.Description = updatedItem.Description;
        todo.IsCompleted = updatedItem.IsCompleted;

        await _context.SaveChangesAsync(); // Save the changes
        return Ok(todo);
    }

    // DELETE: Permanently removes a task
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync(); // Finalize the deletion
        return NoContent();
    }
}