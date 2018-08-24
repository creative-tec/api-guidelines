namespace CT.ApiGuidelines.Domain.Account
{
    using Core.Common;

    public class TransactionType : Enumeration
    {
        public static readonly TransactionType Withdrawal = new TransactionType(1, "Withdrawal");
        public static readonly TransactionType Deposit = new TransactionType(1, "Deposit");

        public TransactionType(int value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
