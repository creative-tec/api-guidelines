namespace CT.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class ResultCommonLogic
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _error;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int _code;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<IValidationResult> _validationResults;

        [DebuggerStepThrough]
        public ResultCommonLogic(bool isFailure, string error)
        {
            if (isFailure)
            {
                if (string.IsNullOrEmpty(error))
                {
                    throw new ArgumentNullException(nameof(error), "There must be error message for failure.");
                }
            }
            else
            {
                if (error != null)
                {
                    throw new ArgumentException("There should be no error message for success.", nameof(error));
                }
            }

            IsFailure = isFailure;
            _error = error;
            _code = isFailure ? Result.DefaultErrorCode : Result.DefaultOkCode;
            _validationResults = new List<IValidationResult>();
        }

        [DebuggerStepThrough]
        public ResultCommonLogic(bool isFailure, string error, int code)
            : this(isFailure, error)
        {
            if (code == 0)
            {
                throw new ArgumentNullException(nameof(code), "The Code must be not be 0.");
            }

            _code = code;
            _validationResults = new List<IValidationResult>();
        }

        [DebuggerStepThrough]
        public ResultCommonLogic(
            bool isFailure,
            string error,
            IEnumerable<IValidationResult> validationResults)
            : this(isFailure, error, isFailure ? Result.DefaultErrorCode : Result.DefaultOkCode, validationResults)
        {
        }

        [DebuggerStepThrough]
        public ResultCommonLogic(bool isFailure, string error, int code, IEnumerable<IValidationResult> validationResults)
            : this(isFailure, error, code)
        {
            if (validationResults == null)
            {
                _validationResults = new List<IValidationResult>();
                return;
            }

            _validationResults = validationResults.ToList();
        }

        public string Error
        {
            [DebuggerStepThrough]
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException("There is no error message for success.");
                }

                return _error;
            }
        }

        public IEnumerable<IValidationResult> ValidationResults
        {
            [DebuggerStepThrough]
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException("There is no error message for success.");
                }

                return _validationResults;
            }
        }

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        public int Code => _code;
    }
}
