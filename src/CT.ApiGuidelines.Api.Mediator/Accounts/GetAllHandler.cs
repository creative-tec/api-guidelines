namespace CT.ApiGuidelines.Api.Mediator.Accounts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Common;
    using Domain.Account;
    using Domain.Repositories;
    using MediatR;
    using Models.Accounts;

    public class GetAllHandler : IRequestHandler<GetAllQuery, IEnumerable<AccountGetV1>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapTo<AccountGetV1, Account> _accountMapper;

        public GetAllHandler(IAccountRepository accountRepository, IMapTo<AccountGetV1, Account> accountMapper)
        {
            _accountRepository = accountRepository;
            _accountMapper = accountMapper;
        }

        public async Task<IEnumerable<AccountGetV1>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.FindAll().ConfigureAwait(false);

            return accounts.Select(c => _accountMapper.MapTo(c));
        }
    }
}
