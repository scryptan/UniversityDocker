using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using UniversityDocker;
using UniversityDocker.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddSingleton<IAppSettings>(new AppSettings
{
    DbConnectionString = Environment.GetEnvironmentVariable("DbConnectionString")!
});
builder.Services.AddSingleton<DbContextFactory>();

var app = builder.Build();
app.MapControllers();

app.Services.GetService<DbContextFactory>()!.Create().Database.EnsureCreated();
app.Services.GetService<DbContextFactory>()!.Create().Database.Migrate();

app.Run();