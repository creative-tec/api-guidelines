namespace CT.Core.Common
{
    using System;

    public struct ValidationResult : IValidationResult
    {
        internal ValidationResult(string item, int errorCode, string error)
        {
            if (string.IsNullOrEmpty(item))
            {
                throw new ArgumentNullException(nameof(item), "There must be item.");
            }

            if (string.IsNullOrEmpty(error))
            {
                throw new ArgumentNullException(nameof(item), "There must be error.");
            }

            if (errorCode == 0)
            {
                throw new ArgumentNullException(nameof(errorCode), "There must be error code must not be 0.");
            }

            Item = item;
            ErrorCode = errorCode;
            Error = error;
        }

        public string Item { get; }

        public int ErrorCode { get; }

        public string Error { get; }
    }
}
