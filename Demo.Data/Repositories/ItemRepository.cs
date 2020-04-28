using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Core.Dto;
using Demo.Core.Repository;
using Demo.Data.Factories;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Demo.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        protected IRepositoryContextFactory ContextFactory { get; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ItemRepository(IRepositoryContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<GetItemsResponse> GetItemsAsync(GetItemsRequest request)
        {
            try
            {
                Logger.Trace("GetItems start, request={@request}", request);

                if (request.PageNumber < 1 || request.PageSize < 1)
                    return new GetItemsResponse(false);

                using (var context = ContextFactory.CreateDbContext())
                {
                    return new GetItemsResponse(true, 
                        await context.Items
                            .Where(item => item.ParentId.Equals(request.ParentGuid))
                            .OrderBy(item => item.Title)
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToListAsync());
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception, exception.Message);

                return new GetItemsResponse(false);
            }
        }

        public async Task CleanRepositoryAsync()
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                if (!context.Database.IsSqlServer()) return;

                var mapping = context.Model.FindEntityType(typeof(Item)).Relational();
                var tableName = mapping.TableName;

                string sqlString = $"TRUNCATE TABLE {tableName}";
                await context.Database.ExecuteSqlCommandAsync(sqlString);
            }
        }

        public async Task FillRepositoryAsync(IItemGenerator generator)
        {
            Logger.Trace("FillRepository start");

            List<Item> items = generator.GenerateItems().Cast<Item>().ToList();
            using (var context = ContextFactory.CreateDbContext())
            {
                if (context.Database.IsSqlServer())
                {
                    await context.BulkInsertAsync(items, new BulkConfig {BulkCopyTimeout = 600});
                    Logger.Trace($"FillRepository: {items.Count} items has been inserted into DB");
                }
                else
                {
                    context.Items.AddRange(items);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
