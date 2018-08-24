namespace CT.ApiGuidelines.Api.Mediator.Accounts
{
    using System.Collections.Generic;
    using MediatR;
    using Models.Accounts;

    public class GetAllQuery : IRequest<IEnumerable<AccountGetV1>>
    {
    }
}
