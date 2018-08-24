namespace CT.ApiGuidelines.Domain.Account
{
    using Core.Common;
    using EnsureThat;

    public class AccountReference : ValueObject<AccountReference>
    {
        public AccountReference(string prefix, int number)
        {
            Ensure.String.HasLengthBetween(prefix, 2, 2, nameof(prefix));
            EnsureArg.IsInRange(number, 1, 9999999, nameof(number));

            Value = $"{prefix}{number}";
        }

        public string Value { get; }

        protected override bool EqualsCore(AccountReference other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
