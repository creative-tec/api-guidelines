namespace CT.Core.Common
{
    public interface IValidationResult
    {
        string Item { get; }

        int ErrorCode { get; }

        string Error { get; }
    }
}
