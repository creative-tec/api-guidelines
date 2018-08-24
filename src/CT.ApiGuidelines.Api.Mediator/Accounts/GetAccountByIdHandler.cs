namespace CT.ApiGuidelines.Api.Mediator.Accounts
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Common;
    using Domain.Account;
    using Domain.Repositories;
    using MediatR;
    using Models.Accounts;

    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, Maybe<AccountGetV1>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapTo<AccountGetV1, Account> _accountMapper;

        public GetAccountByIdHandler(IAccountRepository accountRepository, IMapTo<AccountGetV1, Account> accountMapper)
        {
            _accountRepository = accountRepository;
            _accountMapper = accountMapper;
        }

        public async Task<Maybe<AccountGetV1>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindById(request.AccountId).ConfigureAwait(false);

            return account.HasValue ? _accountMapper.MapTo(account.Value) : new Maybe<AccountGetV1>();
        }
    }
}
