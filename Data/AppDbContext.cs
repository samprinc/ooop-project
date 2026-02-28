using Microsoft.EntityFrameworkCore;
using SimpleTodoAPI.Models;

namespace SimpleTodoAPI.Data;

// TWEAK: Added comments for your presentation!
// AppDbContext is the "Bridge" between our C# code and the SQL Database.
// By inheriting from 'DbContext', we get all of Entity Framework's built-in database powers.
public class AppDbContext : DbContext
{
    // This constructor catches the connection string (the database URL) that we set up in Program.cs
    // and passes it to the base DbContext so it knows exactly WHICH database to log into.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet represents an actual Table inside our SQL database.
    // This line tells Entity Framework: "Look at my TodoItem model, and create a SQL table called 'TodoItems' with those exact columns."
    public DbSet<TodoItem> TodoItems { get; set; }
}