using Microsoft.EntityFrameworkCore;
using UniversityDocker.Entities;
using UniversityDocker.Settings;

namespace UniversityDocker;

public sealed class AppDbContext: DbContext
{
    private readonly IAppSettings _appSettings;
    public DbSet<Book> Books { get; set; } = null!;
    
    public AppDbContext(IAppSettings appSettings)
    {
        _appSettings = appSettings;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_appSettings.DbConnectionString);
    }
}