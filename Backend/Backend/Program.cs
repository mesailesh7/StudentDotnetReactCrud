using Backend.Models;
using Microsoft.EntityFrameworkCore;


//cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        // policy.WithOrigins("http://example.com", "http://www.contosoco.com");
        // policy.WithOrigins("*");
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyMethod() //if not mentioned only get will be allowed
        .AllowAnyHeader(); // x-pagination
    });
});


//services
// Controller based api
builder.Services.AddControllers();



//Connection string can be defined like this as well but its not a good pracitse so we define connection string in appsettings.json and pull it from there
// string connectionString = "Data Source = Person.db";
string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new ArgumentException("Connection string is null");
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlite(connectionString));

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
//middlewares
// app.MapGet("/", () => "Hello World!");
app.MapControllers();


app.Run();