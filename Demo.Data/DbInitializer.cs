using System.Threading.Tasks;
using Demo.Core.Repository;
using Demo.Data.Factories;
using Demo.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IRepositoryContextFactory repositoryContextFactory)
        {
            using (var context = repositoryContextFactory.CreateDbContext())
            {
                await context.Database.MigrateAsync();

                var itemsCount = await context.Items.CountAsync();
                if (itemsCount == 0)
                {
                    IItemRepository itemRepository = new ItemRepository(repositoryContextFactory);
                    IGenerationRule generationRule = new Random500KGenerationRule();

                    await itemRepository.FillRepositoryAsync(new ItemGenerator(generationRule));
                }
            }
        }
    }
}
