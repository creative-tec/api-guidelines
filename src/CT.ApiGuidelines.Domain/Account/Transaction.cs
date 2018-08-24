namespace CT.ApiGuidelines.Domain.Account
{
    using System;
    using Core.Common;

    public class Transaction : ChildEntity<int>
    {
        public Transaction(decimal amount, TransactionType type, DateTime date)
        {
            Amount = amount;
            Type = type;
            Date = date;
        }

        public decimal Amount { get; }

        public TransactionType Type { get; }

        public DateTime Date { get; }
    }
}
