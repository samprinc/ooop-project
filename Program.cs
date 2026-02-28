using Microsoft.EntityFrameworkCore;
using SimpleTodoAPI.Data; // TWEAK: This connects your AppDbContext so Program.cs knows where it is

var builder = WebApplication.CreateBuilder(args);

// TWEAK: Added presentation comments!
// This is our Dependency Injection setup. We are telling the application: 
// "Whenever a controller asks for a database connection, use SQL Server and grab the connection details (DefaultConnection) from our appsettings.json file."
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // FIX: Added this back so the app knows how to generate the Swagger documentation!

var app = builder.Build();

// FIX: Added the Swagger middleware back so you don't get that 404 error!
// This tells the app to actually display the Swagger UI page when we run it.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();