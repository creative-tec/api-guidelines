namespace CT.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerationExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            var returnTs = new List<T>();

            @this.ToList().ForEach(
                t =>
                    {
                        returnTs.Add(t);
                        action(t);
                    });

            return returnTs;
        }

        public static TResult Using<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, TResult> map)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return map(disposable);
            }
        }
    }
}
