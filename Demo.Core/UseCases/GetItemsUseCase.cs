using System.Threading.Tasks;
using Demo.Core.Dto;
using Demo.Core.Repository;
using Demo.Core.UseCases.CommonInterface;

namespace Demo.Core.UseCases
{
    /// <summary>
    /// class to process GetItems use case
    /// </summary>
    public class GetItemsUseCase : IGetItemsUseCase
    {
        private readonly IItemRepository _itemRepository;

        public GetItemsUseCase(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task Handle(GetItemsRequest request, IOutputPort<GetItemsResponse> outputPort)
        {
            GetItemsResponse response = await _itemRepository.GetItemsAsync(request);

            outputPort.Handle(response);
        }
    }
}
