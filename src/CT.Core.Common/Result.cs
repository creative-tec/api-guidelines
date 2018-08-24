namespace CT.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public struct Result : IResult
    {
        public const int DefaultErrorCode = 500;
        public const int DefaultOkCode = 200;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic _logic;

        [DebuggerStepThrough]
        private Result(bool isFailure, string error)
        {
            _logic = new ResultCommonLogic(isFailure, error);
        }

        [DebuggerStepThrough]
        private Result(bool isFailure, string error, int errorCode)
        {
            _logic = new ResultCommonLogic(isFailure, error, errorCode);
        }

        [DebuggerStepThrough]
        private Result(bool isFailure, string error, IEnumerable<IValidationResult> validationResults)
        {
            _logic = new ResultCommonLogic(isFailure, error, validationResults);
        }

        [DebuggerStepThrough]
        private Result(bool isFailure, string error, int errorCode, IEnumerable<IValidationResult> validationResults)
        {
            _logic = new ResultCommonLogic(isFailure, error, errorCode, validationResults);
        }

        public IEnumerable<IValidationResult> ValidationResults => _logic.ValidationResults;

        public int Code => _logic.Code;

        public string Error => _logic.Error;

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        [DebuggerStepThrough]
        public static Result Combine(string errorMessagesSeparator, params IResult[] results)
        {
            List<IResult> failedResults = results.Where(x => x.IsFailure).ToList();

            if (!failedResults.Any())
            {
                return Ok();
            }

            string errorMessage = string.Join(errorMessagesSeparator, failedResults.Select(x => x.Error).ToArray());
            return Fail(errorMessage);
        }

        [DebuggerStepThrough]
        public static Result Combine(params IResult[] results)
        {
            return Combine(",", results);
        }

        [DebuggerStepThrough]
        public static Result Fail(IResult result)
        {
            return new Result(true, result.Error, result.Code, result.ValidationResults);
        }

        [DebuggerStepThrough]
        public static Result Fail(string error)
        {
            return new Result(true, error);
        }

        [DebuggerStepThrough]
        public static Result Fail(string error, int errorCode)
        {
            return new Result(true, error, errorCode);
        }

        [DebuggerStepThrough]
        public static Result Fail(string error, IEnumerable<IValidationResult> validationResults)
        {
            return new Result(true, error, validationResults);
        }

        [DebuggerStepThrough]
        public static Result Fail(string error, int errorCode, IEnumerable<IValidationResult> validationResults)
        {
            return new Result(true, error, errorCode, validationResults);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(IResult<T> result)
        {
            return new Result<T>(true, default(T), result.Error, result.Code, result.ValidationResults);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error)
        {
            return new Result<T>(true, default(T), error);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error, int errorCode)
        {
            return new Result<T>(true, default(T), error, errorCode);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error, IEnumerable<IValidationResult> validationResults)
        {
            return new Result<T>(true, default(T), error, validationResults);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error, int errorCode, IEnumerable<IValidationResult> validationResults)
        {
            return new Result<T>(true, default(T), error, errorCode, validationResults);
        }

        [DebuggerStepThrough]
        public static IValidationResult CreateValidationResult(string item, string error)
        {
            return new ValidationResult(item, DefaultErrorCode, error);
        }

        [DebuggerStepThrough]
        public static IValidationResult CreateValidationResult(string item, int errorCode, string error)
        {
            return new ValidationResult(item, errorCode, error);
        }

        [DebuggerStepThrough]
        public static Result FirstFailureOrSuccess(params IResult[] results)
        {
            foreach (IResult result in results)
            {
                if (result.IsFailure)
                {
                    return Fail(result.Error);
                }
            }

            return Ok();
        }

        [DebuggerStepThrough]
        public static Result Ok()
        {
            return new Result(false, null);
        }

        [DebuggerStepThrough]
        public static Result Ok(int code)
        {
            return new Result(false, null, code);
        }

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(false, value, null);
        }

        public static Result<T> Ok<T>(T value, int code)
        {
            return new Result<T>(false, value, null, code);
        }
    }

    public struct Result<T> : IResult<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic _logic;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string error)
        {
            if (!isFailure && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _logic = new ResultCommonLogic(isFailure, error);
            _value = value;
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string error, int errorCode)
        {
            if (!isFailure && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _logic = new ResultCommonLogic(isFailure, error, errorCode);
            _value = value;
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string error, IEnumerable<IValidationResult> validationResults)
        {
            if (!isFailure && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _logic = new ResultCommonLogic(isFailure, error, validationResults);
            _value = value;
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string error, int errorCode, IEnumerable<IValidationResult> validationResults)
        {
            if (!isFailure && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _logic = new ResultCommonLogic(isFailure, error, errorCode, validationResults);
            _value = value;
        }

        public IEnumerable<IValidationResult> ValidationResults => _logic.ValidationResults;

        public int Code => _logic.Code;

        public string Error => _logic.Error;

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (!IsSuccess)
                {
                    throw new InvalidOperationException("There is no value for failure.");
                }

                return _value;
            }
        }
    }
}
