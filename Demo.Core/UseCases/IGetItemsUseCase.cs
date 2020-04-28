using Demo.Core.Dto;
using Demo.Core.UseCases.CommonInterface;

namespace Demo.Core.UseCases
{
    /// <summary>
    /// interface for get items use case
    /// </summary>
    public interface IGetItemsUseCase : IUseCaseRequestHandler<GetItemsRequest, GetItemsResponse>
    {
    }
}
