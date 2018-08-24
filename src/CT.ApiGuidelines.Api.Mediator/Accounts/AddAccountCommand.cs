namespace CT.ApiGuidelines.Api.Mediator.Accounts
{
    using Core.Common;
    using EnsureThat;
    using MediatR;
    using Models.Accounts;

    public class AddAccountCommand : IRequest<IResult<AccountGetV1>>
    {
        public AddAccountCommand(AccountPostV1 accountPostModel)
        {
            AccountPostModel = EnsureArg.IsNotNull(accountPostModel, nameof(accountPostModel));
        }

        public AccountPostV1 AccountPostModel { get; }
    }
}
