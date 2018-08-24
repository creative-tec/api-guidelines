namespace CT.ApiGuidelines.Domain.Account
{
    using System;
    using System.Collections.Generic;
    using Core.Common;
    using EnsureThat;
    using Exceptions;

    public sealed class Account : RootEntity<Guid>
    {
        public Account(Guid id, AccountReference reference, Guid ownerId)
        {
            Id = Ensure.Guid.IsNotEmpty(id, nameof(id));
            OwnerId = Ensure.Guid.IsNotEmpty(ownerId, nameof(ownerId));
            Reference = EnsureArg.IsNotNull(reference, nameof(reference));
            Balance = 0;
            Status = AccountStatus.Open;
            Transactions = new List<Transaction>();
        }

        public Guid OwnerId { get; }

        public IList<Transaction> Transactions { get; }

        public AccountReference Reference { get; }

        public decimal Balance { get; private set; }

        public AccountStatus Status { get; private set; }

        public bool CanDeposit => Status.Equals(AccountStatus.Open);

        public bool CanWithdrawal => Status.Equals(AccountStatus.Open) && Balance > 0;

        public void Withdraw(decimal amount)
        {
            if (Balance - amount < 0 || amount < 0)
            {
                throw new AccountWithdrawalException();
            }

            Balance -= amount;

            Transactions.Add(new Transaction(amount, TransactionType.Withdrawal, DateTime.UtcNow.Date));
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new AccountDepositException();
            }

            Balance += amount;

            Transactions.Add(new Transaction(amount, TransactionType.Deposit, DateTime.UtcNow.Date));
        }

        public void Close()
        {
            if (Balance > 0)
            {
                throw new AccountCloseException();
            }

            Status = AccountStatus.Closed;
        }
    }
}
