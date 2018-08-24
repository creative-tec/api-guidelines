namespace CT.Core.Common
{
    using System;

    public static class ResultExtensions
    {
        public static IResult<T> Ensure<T>(this IResult<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result);
            }

            if (!predicate(result.Value))
            {
                return Result.Fail<T>(errorMessage);
            }

            return Result.Ok(result.Value);
        }

        public static IResult Ensure(this IResult result, Func<bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result);
            }

            if (!predicate())
            {
                return Result.Fail(errorMessage);
            }

            return Result.Ok();
        }

        public static IResult<TK> Map<T, TK>(this IResult<T> result, Func<T, TK> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            return Result.Ok(func(result.Value));
        }

        public static IResult<T> Map<T>(this IResult result, Func<T> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            return Result.Ok(func());
        }

        public static T OnBoth<T>(this IResult result, Func<IResult, T> func)
        {
            return func(result);
        }

        public static TK OnBoth<T, TK>(this IResult<T> result, Func<IResult<T>, TK> func)
        {
            return func(result);
        }

        public static IResult<TK> OnSuccess<T, TK>(this IResult<T> result, Func<T, TK> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            return Result.Ok(func(result.Value));
        }

        public static IResult<T> OnSuccess<T>(this IResult result, Func<T> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            return Result.Ok(func());
        }

        public static IResult<TK> OnSuccess<T, TK>(this IResult<T> result, Func<T, IResult<TK>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            return func(result.Value);
        }

        public static IResult<T> OnSuccess<T>(this IResult result, Func<IResult<T>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            return func();
        }

        public static IResult<TK> OnSuccess<T, TK>(this IResult<T> result, Func<IResult<TK>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            return func();
        }

        public static IResult OnSuccess<T>(this IResult<T> result, Func<T, IResult> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error, result.Code, result.ValidationResults);
            }

            return func(result.Value);
        }

        public static IResult OnSuccess(this IResult result, Func<IResult> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return func();
        }

        public static IResult<T> OnSuccess<T>(this IResult<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static IResult OnSuccess(this IResult result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static IResult<T> OnFailure<T>(this IResult<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static IResult OnFailure(this IResult result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static IResult OnFailure(this IResult result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static IResult<T> OnFailure<T>(this IResult<T> result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static IResult OnFailure(this IResult result, Action<IResult> action)
        {
            if (result.IsFailure)
            {
                action(result);
            }

            return result;
        }

        public static IResult<T> OnFailure<T>(this IResult<T> result, Action<IResult> action)
        {
            if (result.IsFailure)
            {
                action(result);
            }

            return result;
        }
    }
}
