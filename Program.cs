using Microsoft.EntityFrameworkCore;
using SimpleTodoAPI.Data; 

var builder = WebApplication.CreateBuilder(args);

// Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// **Register AppDbContext with DI**
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));
    
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // This enables the Swagger UI

var app = builder.Build();

// Enable the Swagger page so we can test the API easily
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();