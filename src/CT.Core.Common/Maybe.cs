namespace CT.Core.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class Maybe<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _values;

        public Maybe()
        {
            _values = new T[0];
        }

        public Maybe(T value)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            _values = value == null ? new T[0] : new[] { value };
        }

        public bool HasNoValue => !HasValue;

        public bool HasValue => _values.Any();

        public T Value
        {
            get
            {
                if (HasNoValue)
                {
                    throw new InvalidOperationException();
                }

                return _values.Single();
            }
        }

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public static bool operator ==(Maybe<T> maybe, T value)
        {
            return maybe != null && (!maybe.HasNoValue && maybe.Value.Equals(value));
        }

        public static bool operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first != null && first.Equals(second);
        }

        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return !(maybe == value);
        }

        public static bool operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (obj is T)
            {
                obj = new Maybe<T>((T)obj);
            }

            if (!(obj is Maybe<T>))
            {
                return false;
            }

            var other = (Maybe<T>)obj;
            return Equals(other);
        }

        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return HasNoValue ? 0 : _values.GetHashCode();
        }

        public override string ToString()
        {
            return HasNoValue ? "No value" : Value.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
