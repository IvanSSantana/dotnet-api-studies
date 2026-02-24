using API.Configurations;
using EvolveDb;
using Testcontainers.PostgreSql;

namespace API.Tests.Integration.Tools
{
    public class PostgreSQLFixture : IAsyncLifetime
    {
        public PostgreSqlContainer _container { get; }

        public PostgreSQLFixture()
        {
            _container = new PostgreSqlBuilder("postgres:latest")
                .WithDatabase("database")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .WithCleanUp(true)
                .Build();
        }


        public async Task InitializeAsync()
        {
            await _container.StartAsync();
            MigrationsConfig.ExecuteMigrations(_container.GetConnectionString());
        }

        public async Task DisposeAsync()
        {
            await _container.DisposeAsync();    

        }

        public string ConnectionString => _container.GetConnectionString();
    }
}