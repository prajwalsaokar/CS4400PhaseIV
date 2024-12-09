using BusinessAPI.Config;
using BusinessAPI.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseOptions"));
Console.WriteLine($"Connected to: {builder.Configuration.GetSection("DatabaseOptions:ConnectionString").Value}");
// Register classes
builder.Services.AddScoped<CoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.MapRazorPages();

app.Run();

