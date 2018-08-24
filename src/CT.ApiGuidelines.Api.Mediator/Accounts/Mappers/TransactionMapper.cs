namespace CT.ApiGuidelines.Api.Mediator.Accounts.Mappers
{
    using Core.Common;
    using Domain.Account;
    using Models.Accounts;

    public class TransactionMapper : IMapTo<TransactionGetV1, Transaction>
    {
        public TransactionGetV1 MapTo(Transaction item)
        {
            return new TransactionGetV1
            {
                Amount = item.Amount,
                Date = item.Date,
                TypeValue = item.Type.Value,
                TypeName = item.Type.DisplayName
            };
        }
    }
}
