namespace UniversityDocker.Settings;

public class AppSettings: IAppSettings
{
    public string DbConnectionString { get; set; } = null!;
}