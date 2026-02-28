using Microsoft.EntityFrameworkCore;
using SimpleTodoAPI.Models;

namespace SimpleTodoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> Todos { get; set; } // example table
    }
}