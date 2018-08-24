namespace CT.ApiGuidelines.Api.Mediator.Accounts
{
    using System;
    using Core.Common;
    using MediatR;
    using Models.Accounts;

    public class GetAccountByIdQuery : IRequest<Maybe<AccountGetV1>>
    {
        public GetAccountByIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; }
    }
}
