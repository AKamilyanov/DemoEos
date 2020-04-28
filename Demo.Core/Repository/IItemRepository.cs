using System.Threading.Tasks;
using Demo.Core.Dto;

namespace Demo.Core.Repository
{
    /// <summary>
    /// item repository interface
    /// </summary>
    public interface IItemRepository
    {
        Task<GetItemsResponse> GetItemsAsync(GetItemsRequest request);

        Task CleanRepositoryAsync();

        Task FillRepositoryAsync(IItemGenerator generator);
    }
}
