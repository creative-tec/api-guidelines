namespace CT.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class EnumerationStaticList<TI>
    {
        private readonly TI _value;

        protected EnumerationStaticList()
        {
        }

        protected EnumerationStaticList(TI value, string displayName)
        {
            _value = value;
            DisplayName = displayName;
        }

        public string DisplayName { get; }

        public TI Value => _value;

        public static T FromDisplayName<T>(string displayName)
            where T : EnumerationStaticList<TI>, new()
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        public static T FromValue<T>(TI value)
            where T : EnumerationStaticList<TI>, new()
        {
            var matchingItem = Parse<T, TI>(value, "value", item => item.Value.Equals(value));
            return matchingItem;
        }

        public static IEnumerable<T> GetAll<T>()
            where T : EnumerationStaticList<TI>, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return (from info in fields let instance = new T() select info.GetValue(instance)).OfType<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as EnumerationStaticList<TI>;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = _value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TI>.Default.GetHashCode(_value) * 397)
                       ^ (DisplayName?.GetHashCode() ?? 0);
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        protected bool Equals(EnumerationStaticList<TI> other)
        {
            return EqualityComparer<TI>.Default.Equals(_value, other._value)
                   && string.Equals(DisplayName, other.DisplayName);
        }

        private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate)
            where T : EnumerationStaticList<TI>, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = $"'{value}' is not a valid {description} in {typeof(T)}";
                throw new ApplicationException(message);
            }

            return matchingItem;
        }
    }
}
