using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Tracker.Shared.Services.Configuration;

/// <summary>
/// Handles application configuration loading, file creation and database option setup.
/// </summary>
public class ConfigurationService(Assembly resourceAssembly)
{
    private const string SHARED_ROOT_FOLDER_NAME = "Fang Software";
    private const string APP_FOLDER_NAME = "Tracker";
    private const string APP_SETTINGS_FILE = "appsettings.json";
    private const string RELEASE_DATABASE_FILE_NAME = "tracker.db";
    private const string DEBUG_DATABASE_FILE_NAME = "tracker-dev.db";
    private const string README_FILE = "README.md";

    private IConfiguration? configuration;

    private string GetExecutionDirectory()
    {
        return Environment.CurrentDirectory;
    }

    /// <summary>
    /// Builds and returns the application configuration from the local app data folder.
    /// </summary>
    public IConfiguration BuildConfiguration()
    {
        IConfigurationBuilder configBuilder = new ConfigurationBuilder();

        configBuilder.AddEnvironmentVariables();

        if (!IsRunningInCi())
        {
            EnsureAppSettingsFileExists();
            CopyReadmeFile();

            string fullAppFilePath = Path.Combine(GetApplicationDataPath(), APP_SETTINGS_FILE);

            configBuilder.AddJsonFile(fullAppFilePath, false, true);
        }

        configuration = configBuilder.Build();
        return configuration;
    }

    private void CopyReadmeFile()
    {
        using Stream? stream = resourceAssembly.GetManifestResourceStream(README_FILE);

        if (stream is null)
        {
            return;
        }

        string readmePath = Path.Combine(GetApplicationDataPath(), README_FILE);

        Directory.CreateDirectory(GetApplicationDataPath());

        using FileStream fileStream = File.Create(readmePath);
        stream.CopyTo(fileStream);
    }

    private bool IsRunningInCi()
    {
        return string.Equals(Environment.GetEnvironmentVariable("CI"), "true", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Configures the database options.
    /// </summary>
    public void ConfigureDatabaseOptions(DbContextOptionsBuilder options)
    {
        Directory.CreateDirectory(GetApplicationDataPath());

        string databaseFileName = RELEASE_DATABASE_FILE_NAME;

#if DEBUG
        databaseFileName = DEBUG_DATABASE_FILE_NAME;
#endif

        string databasePath = Path.Combine(GetApplicationDataPath(), databaseFileName);

        options.UseSqlite($"Data Source={databasePath}");

#if DEBUG
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
#endif
    }

    /// <summary>
    /// Returns the application data path used for configuration files.
    /// </summary>
    public string GetApplicationDataPath()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            SHARED_ROOT_FOLDER_NAME, APP_FOLDER_NAME);
    }

    /// <summary>
    /// Ensures the appsettings file exists, creating an empty one if needed.
    /// </summary>
    /// <summary>
    /// Ensures the appsettings file exists and contains required sections.
    /// </summary>
    private void EnsureAppSettingsFileExists()
    {
        string fullAppFilePath = Path.Combine(GetApplicationDataPath(), APP_SETTINGS_FILE);

        if (!File.Exists(fullAppFilePath))
        {
            CreateFile(fullAppFilePath, CreateDefaultAppSettings());
            return;
        }
    }

    private object CreateDefaultAppSettings()
    {
        return new
        {
        };
    }

    /// <summary>
    /// Creates a JSON file with the supplied template content.
    /// </summary>
    private void CreateFile(string path, object template)
    {
        string templateContent = JsonSerializer.Serialize(template, new JsonSerializerOptions
        {
            WriteIndented = true,
        });

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, templateContent);
    }
}
