namespace CT.ApiGuidelines.Api.Models.Accounts
{
    using System;

    public class TransactionGetV1
    {
        public decimal Amount { get; set; }

        public int TypeValue { get; set; }

        public string TypeName { get; set; }

        public DateTime Date { get; set; }
    }
}
