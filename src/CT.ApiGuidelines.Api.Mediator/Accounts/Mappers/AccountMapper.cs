namespace CT.ApiGuidelines.Api.Mediator.Accounts.Mappers
{
    using System.Collections.Generic;
    using Core.Common;
    using Domain.Account;
    using Models.Accounts;

    public class AccountMapper : IMapTo<AccountGetV1, Account>
    {
        private readonly IMapTo<TransactionGetV1, Transaction> _transactionMapper;

        public AccountMapper(IMapTo<TransactionGetV1, Transaction> transactionMapper)
        {
            _transactionMapper = transactionMapper;
        }

        public AccountGetV1 MapTo(Account item)
        {
            var accountDto = new AccountGetV1
            {
                Id = item.Id,
                OwnerId = item.OwnerId,
                Reference = item.Reference.Value,
                Balance = item.Balance,
                StatusValue = item.Status.Value,
                StatusName = item.Status.DisplayName
            };

            var transactionDtos = new List<TransactionGetV1>();

            item.Transactions.ForEach(t => transactionDtos.Add(_transactionMapper.MapTo(t)));

            accountDto.Transactions = transactionDtos;

            return accountDto;
        }
    }
}
