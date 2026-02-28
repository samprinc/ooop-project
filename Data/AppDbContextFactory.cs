using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleTodoAPI.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Make sure the path matches your SQLite file
            optionsBuilder.UseSqlite("Data Source=Sqlite.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}