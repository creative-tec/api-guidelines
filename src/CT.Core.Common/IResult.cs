namespace CT.Core.Common
{
    using System.Collections.Generic;

    public interface IResult
    {
        IEnumerable<IValidationResult> ValidationResults { get; }

        int Code { get; }

        string Error { get; }

        bool IsFailure { get; }

        bool IsSuccess { get; }
    }

    public interface IResult<out T> : IResult
    {
        T Value { get; }
    }
}
