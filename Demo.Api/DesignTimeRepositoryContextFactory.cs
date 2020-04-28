using System.IO;
using Demo.Data;
using Demo.Data.Factories;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Demo.Api
{
    // Just for migration
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("LocalSqlConnection");
            var repositoryFactory = new RepositoryContextFactory(connectionString);

            return repositoryFactory.CreateDbContext();
        }
    }
}
