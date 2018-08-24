namespace CT.ApiGuidelines.Api.Mediator.Accounts
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Common;
    using Domain.Account;
    using Domain.Repositories;
    using MediatR;
    using Models.Accounts;

    public class AddAccountHandler : IRequestHandler<AddAccountCommand, IResult<AccountGetV1>>
    {
        private static readonly Random Random = new Random();
        private readonly IAccountRepository _accountRepository;
        private readonly IMapTo<AccountGetV1, Account> _accountMapper;
        private readonly IOwnerRepository _ownerRepository;

        public AddAccountHandler(
            IAccountRepository accountRepository,
            IMapTo<AccountGetV1, Account> accountMapper,
            IOwnerRepository ownerRepository)
        {
            _accountRepository = accountRepository;
            _accountMapper = accountMapper;
            _ownerRepository = ownerRepository;
        }

        public async Task<IResult<AccountGetV1>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.FindById(request.AccountPostModel.OwnerId).ConfigureAwait(false);

            if (owner.HasNoValue)
            {
                return Result.Fail<AccountGetV1>($"Could not find owner with OwnerId {request.AccountPostModel.OwnerId}");
            }

            var account = new Account(Guid.NewGuid(), new AccountReference("FW", Random.Next(1, 9999999)), owner.Value.Id);

            _accountRepository.Add(account);

            await _accountRepository.SaveChanges().ConfigureAwait(false);

            var newAccountDto = _accountMapper.MapTo(account);

            return Result.Ok(newAccountDto);
        }
    }
}
