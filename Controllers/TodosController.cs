using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // TWEAK: Added this to use async database commands like ToListAsync()
using SimpleTodoAPI.Models;
using SimpleTodoAPI.Data; // TWEAK: Added this so the controller can find AppDbContext

namespace SimpleTodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    // FIX: Removed the static list (_todos). 
    // We now declare a private variable to hold our real database context.
    private readonly AppDbContext _context;

    // TWEAK: Added a constructor. 
    // This is called "Dependency Injection". It automatically hands the live database connection to this controller when the app runs.
    public TodosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/todos/{id}
    [HttpGet("{id}")]
    // TWEAK: Changed to 'async Task<IActionResult>'. Database calls take time, so making it 'async' prevents the app from freezing while it waits for SQL.
    public async Task<IActionResult> GetTodo(int id)
    {
        // FIX: Replaced _todos.FirstOrDefault() with _context.TodoItems.FindAsync(id).
        // FindAsync is a built-in EF Core method to quickly find a record in the database by its Primary Key.
        var todo = await _context.TodoItems.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound($"Todo item with ID {id} not found.");
        }
        return Ok(todo);
    }

    // GET: api/todos
    [HttpGet]
    public async Task<IActionResult> GetAllTodos()
    {
        // FIX: Replaced returning the static list with querying the real database.
        // ToListAsync() fetches all records from the TodoItems table in your SQL database.
        var todos = await _context.TodoItems.ToListAsync();
        return Ok(todos);
    }

    // POST: api/todos
    [HttpPost]
    public async Task<IActionResult> AddTodo(TodoItem newItem)
    {
        // FIX: Removed 'newItem.Id = _todos.Count + 1;' 
        // We don't need to calculate IDs manually anymore because the SQL database automatically generates and increments the ID for us!
        
        _context.TodoItems.Add(newItem); // Stages the new item to be added
        
        // TWEAK: Added SaveChangesAsync(). 
        // When using a real database, nothing actually saves until you call this command!
        await _context.SaveChangesAsync(); 
        
        return Ok(newItem);
    }

    // PUT: api/todos/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, TodoItem updatedItem)
    {
        // Step 1: Find the existing item in the real database
        var todo = await _context.TodoItems.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound($"Todo item with ID {id} not found.");
        }

        // Step 2: Update the properties (your logic here was already perfect)
        todo.Title = updatedItem.Title;
        todo.Description = updatedItem.Description;
        todo.IsCompleted = updatedItem.IsCompleted;
        todo.Email = updatedItem.Email;

        // FIX: We must tell the database to permanently save these new changes.
        await _context.SaveChangesAsync();

        return Ok(todo);
    }

    // DELETE: api/todos/{id}
    // TWEAK: I added a Delete method for you! Since you are using a real database now, you will definitely want the ability to delete records.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null)
        {
            return NotFound($"Todo item with ID {id} not found.");
        }

        _context.TodoItems.Remove(todo); // Stages the item for deletion
        await _context.SaveChangesAsync(); // Executes the deletion in SQL

        return NoContent(); // 204 No Content is the standard success response for a Delete action
    }
}