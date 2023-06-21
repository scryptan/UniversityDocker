using UniversityDocker.Settings;

namespace UniversityDocker;

public class DbContextFactory
{
    private readonly IAppSettings _appSettings;

    public DbContextFactory(IAppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public AppDbContext Create()
    {
        return new AppDbContext(_appSettings);
    }
}