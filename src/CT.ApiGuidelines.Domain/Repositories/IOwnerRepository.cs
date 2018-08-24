namespace CT.ApiGuidelines.Domain.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Core.Common;
    using Owner;

    public interface IOwnerRepository
    {
        Task<Maybe<Owner>> FindById(Guid id);
    }
}
