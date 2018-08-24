namespace CT.ApiGuidelines.Domain.Account
{
    using Core.Common;

    public class AccountStatus : Enumeration
    {
        public static readonly AccountStatus Open = new AccountStatus(1, "Open");
        public static readonly AccountStatus Closed = new AccountStatus(1, "Closed");

        public AccountStatus(int value,  string displayName)
            : base(value,  displayName)
        {
        }
    }
}
