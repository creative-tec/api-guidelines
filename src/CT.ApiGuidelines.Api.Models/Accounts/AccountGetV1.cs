namespace CT.ApiGuidelines.Api.Models.Accounts
{
    using System;
    using System.Collections.Generic;

    public class AccountGetV1
    {
        public AccountGetV1()
        {
            Transactions = new List<TransactionGetV1>();
        }

        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public string Reference { get; set; }

        public decimal Balance { get; set; }

        public int StatusValue { get; set; }

        public string StatusName { get; set; }

        public IEnumerable<TransactionGetV1> Transactions { get; set; }
    }
}
