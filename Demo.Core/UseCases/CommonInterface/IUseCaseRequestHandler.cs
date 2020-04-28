using System.Threading.Tasks;

namespace Demo.Core.UseCases.CommonInterface
{
    /// <summary>
    /// interface for generic use case request handler
    /// </summary>
    public interface IUseCaseRequestHandler<in TUseCaseRequest, out TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
    {
        Task Handle(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);
    }

}
