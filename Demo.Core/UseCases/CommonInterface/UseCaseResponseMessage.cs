namespace Demo.Core.UseCases.CommonInterface
{
    /// <summary>
    /// base class for generic use case response
    /// </summary>
    public abstract class UseCaseResponseMessage
    {
        public bool Success { get; }

        protected UseCaseResponseMessage(bool success = false)
        {
            Success = success;
        }
    }
}
