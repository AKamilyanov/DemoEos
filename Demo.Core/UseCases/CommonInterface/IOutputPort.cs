namespace Demo.Core.UseCases.CommonInterface
{
    /// <summary>
    /// output port interface for onion architecture
    /// </summary>
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handle(TUseCaseResponse response);
    }
}
