namespace CT.Core.Common
{
    using System;
    using System.Threading.Tasks;

    public static class ResultExtensionsAsync
    {
        public static async Task<IResult<T>> Ensure<T>(this Task<IResult<T>> resultTask, Func<T, Task<bool>> predicate, string errorMessage)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            if (!await predicate(result.Value).ConfigureAwait(false))
            {
                return Result.Fail<T>(errorMessage);
            }

            return Result.Ok(result.Value);
        }

        public static async Task<IResult> Ensure(this Task<IResult> resultTask, Func<Task<bool>> predicate, string errorMessage)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail(result.Error, result.Code, result.ValidationResults);
            }

            if (!await predicate().ConfigureAwait(false))
            {
                return Result.Fail(errorMessage);
            }

            return Result.Ok();
        }

        public static async Task<IResult<T>> Ensure<T>(this Task<IResult<T>> resultTask, Func<T, bool> predicate, string errorMessage)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Ensure(predicate, errorMessage);
        }

        public static async Task<IResult> Ensure(this Task<IResult> resultTask, Func<bool> predicate, string errorMessage)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Ensure(predicate, errorMessage);
        }

        public static async Task<IResult<T>> Ensure<T>(this IResult<T> result, Func<T, Task<bool>> predicate, string errorMessage)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            if (!await predicate(result.Value).ConfigureAwait(false))
            {
                return Result.Fail<T>(errorMessage);
            }

            return Result.Ok(result.Value);
        }

        public static async Task<IResult> Ensure(this IResult result, Func<Task<bool>> predicate, string errorMessage)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error, result.Code, result.ValidationResults);
            }

            if (!await predicate().ConfigureAwait(false))
            {
                return Result.Fail(errorMessage);
            }

            return Result.Ok();
        }

        public static async Task<IResult<TK>> Map<T, TK>(this Task<IResult<T>> resultTask, Func<T, Task<TK>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            var value = await func(result.Value).ConfigureAwait(false);

            return Result.Ok(value);
        }

        public static async Task<IResult<T>> Map<T>(this Task<IResult> resultTask, Func<Task<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            var value = await func().ConfigureAwait(false);

            return Result.Ok(value);
        }

        public static async Task<IResult<TK>> Map<T, TK>(this Task<IResult<T>> resultTask, Func<T, TK> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Map(func);
        }

        public static async Task<IResult<T>> Map<T>(this Task<IResult> resultTask, Func<T> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Map(func);
        }

        public static async Task<IResult<TK>> Map<T, TK>(this IResult<T> result, Func<T, Task<TK>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            var value = await func(result.Value).ConfigureAwait(false);

            return Result.Ok(value);
        }

        public static async Task<IResult<T>> Map<T>(this IResult result, Func<Task<T>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            var value = await func().ConfigureAwait(false);

            return Result.Ok(value);
        }

        public static async Task<T> OnBoth<T>(this Task<IResult> resultTask, Func<IResult, Task<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await func(result).ConfigureAwait(false);
        }

        public static async Task<TK> OnBoth<T, TK>(this Task<IResult<T>> resultTask, Func<IResult<T>, Task<TK>> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await func(result).ConfigureAwait(false);
        }

        public static async Task<T> OnBoth<T>(this Task<IResult> resultTask, Func<IResult, T> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnBoth(func);
        }

        public static async Task<TK> OnBoth<T, TK>(this Task<IResult<T>> resultTask, Func<IResult<T>, TK> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnBoth(func);
        }

        public static async Task<T> OnBoth<T>(this IResult result, Func<IResult, Task<T>> func)
        {
            return await func(result).ConfigureAwait(false);
        }

        public static async Task<TK> OnBoth<T, TK>(this IResult<T> result, Func<IResult<T>, Task<TK>> func)
        {
            return await func(result).ConfigureAwait(false);
        }

        public static async Task<IResult<T>> OnFailure<T>(this Task<IResult<T>> resultTask, Func<Task> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                await func().ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult> OnFailure(this Task<IResult> resultTask, Func<Task> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                await func().ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult<T>> OnFailure<T>(this Task<IResult<T>> resultTask, Func<string, Task> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                await func(result.Error).ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult<T>> OnFailure<T>(this Task<IResult<T>> resultTask, Action action)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        public static async Task<IResult> OnFailure(this Task<IResult> resultTask, Action action)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        public static async Task<IResult<T>> OnFailure<T>(this Task<IResult<T>> resultTask, Action<string> action)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        public static async Task<IResult> OnFailure(this Task<IResult> resultTask, Action<string> action)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        public static async Task<IResult<T>> OnFailure<T>(this IResult<T> result, Func<Task> func)
        {
            if (result.IsFailure)
            {
                await func().ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult> OnFailure(this IResult result, Func<Task> func)
        {
            if (result.IsFailure)
            {
                await func().ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult<T>> OnFailure<T>(this IResult<T> result, Func<string, Task> func)
        {
            if (result.IsFailure)
            {
                await func(result.Error).ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult<T>> OnFailure<T>(this IResult<T> result, Func<IResult, Task> func)
        {
            if (result.IsFailure)
            {
                await func(result).ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this Task<IResult<T>> resultTask, Func<T, Task<TK>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            var value = await func(result.Value).ConfigureAwait(false);

            return Result.Ok(value);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this Task<IResult> resultTask, Func<Task<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            var value = await func().ConfigureAwait(false);

            return Result.Ok(value);
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this Task<IResult<T>> resultTask, Func<T, Task<IResult<TK>>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this Task<IResult> resultTask, Func<Task<IResult<T>>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            return await func().ConfigureAwait(false);
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this Task<IResult<T>> resultTask, Func<Task<IResult<TK>>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            return await func().ConfigureAwait(false);
        }

        public static async Task<IResult> OnSuccess<T>(this Task<IResult<T>> resultTask, Func<T, Task<IResult>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail(result.Error, result.Code, result.ValidationResults);
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        public static async Task<IResult> OnSuccess(this Task<IResult> resultTask, Func<Task<IResult>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result;
            }

            return await func().ConfigureAwait(false);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this Task<IResult<T>> resultTask, Func<T, Task> action)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult> OnSuccess(this Task<IResult> resultTask, Func<Task> action)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                await action().ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this Task<IResult<T>> resultTask, Func<T, TK> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this Task<IResult> resultTask, Func<T> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this Task<IResult<T>> resultTask, Func<T, IResult<TK>> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this Task<IResult> resultTask, Func<IResult<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this Task<IResult<T>> resultTask, Func<IResult<TK>> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        public static async Task<IResult> OnSuccess<T>(this Task<IResult<T>> resultTask, Func<T, IResult> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        public static async Task<IResult> OnSuccess(this Task<IResult> resultTask, Func<IResult> func)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this Task<IResult<T>> resultTask, Action<T> action)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(action);
        }

        public static async Task<IResult> OnSuccess(this Task<IResult> resultTask, Action action)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OnSuccess(action);
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this IResult<T> result, Func<T, Task<TK>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            var value = await func(result.Value).ConfigureAwait(false);

            return Result.Ok(value);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this IResult result, Func<Task<T>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            var value = await func().ConfigureAwait(false);

            return Result.Ok(value);
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this IResult<T> result, Func<T, Task<IResult<TK>>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this IResult result, Func<Task<IResult<T>>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error, result.Code, result.ValidationResults);
            }

            return await func().ConfigureAwait(false);
        }

        public static async Task<IResult<TK>> OnSuccess<T, TK>(this IResult<T> result, Func<Task<IResult<TK>>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TK>(result.Error, result.Code, result.ValidationResults);
            }

            return await func().ConfigureAwait(false);
        }

        public static async Task<IResult> OnSuccess<T>(this IResult<T> result, Func<T, Task<IResult>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error, result.Code, result.ValidationResults);
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        public static async Task<IResult> OnSuccess(this IResult result, Func<Task<IResult>> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return await func().ConfigureAwait(false);
        }

        public static async Task<IResult<T>> OnSuccess<T>(this IResult<T> result, Func<T, Task> action)
        {
            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<IResult> OnSuccess(this IResult result, Func<Task> action)
        {
            if (result.IsSuccess)
            {
                await action().ConfigureAwait(false);
            }

            return result;
        }
    }
}