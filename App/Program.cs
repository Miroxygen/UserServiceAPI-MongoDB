using App.Models;
using App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<UserDatabaseSettings>(
    builder.Configuration.GetSection("UserStoreDatabase"));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<DatabaseInitializationService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbInitService = services.GetRequiredService<DatabaseInitializationService>();
    if (!await dbInitService.CheckDatabaseConnectionAsync())
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError("Database connection check failed during startup.");
    } 
    else 
    {
        Console.WriteLine("Succesfully connected to database.");
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Welcome to the user service!");

app.Run();

//"mongodb://mongodb-user-service:27017/User-Service",