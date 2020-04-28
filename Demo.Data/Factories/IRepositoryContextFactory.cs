namespace Demo.Data.Factories
{
    /// <summary>
    /// Interface for factory which produces repositoryContext objects
    /// </summary>
    public interface IRepositoryContextFactory
    {
        RepositoryContext CreateDbContext();
    }
}
