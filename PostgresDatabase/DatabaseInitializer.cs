using DbUp;

namespace WristbandRadio.FileServer.PostgresDatabase;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InitializeAsync()
    {
        EnsureDatabase.For.PostgresqlDatabase(_connectionString);

        var upgrader = DeployChanges.To
            .PostgresqlDatabase(_connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(DatabaseInitializer).Assembly)
            .LogToConsole()
            .Build();

        if (upgrader.IsUpgradeRequired())
        {
            var result = upgrader.PerformUpgrade();
            if (!result.Successful)
            {
                throw new Exception($"Database upgrade failed: {result.Error.Message}");
            }
        }
    }
}
