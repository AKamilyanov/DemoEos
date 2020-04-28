using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Factories
{
    /// <summary>
    /// Factory which deal with Sql Server
    /// </summary>
    public class RepositoryContextFactory : IRepositoryContextFactory
    {
        private readonly string _connectionString;

        public RepositoryContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RepositoryContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            
            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
