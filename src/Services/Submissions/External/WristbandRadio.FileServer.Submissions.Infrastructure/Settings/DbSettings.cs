using Ardalis.GuardClauses;

namespace WristbandRadio.FileServer.Submissions.Infrastructure.Settings;

public class DbSettings
{
    public string ConnectionString { get; set; } = string.Empty;

    public DbSettings(string connectionString)
    {
        ConnectionString = Guard.Against.NullOrEmpty(connectionString);
    }
}

