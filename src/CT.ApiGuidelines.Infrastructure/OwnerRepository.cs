namespace CT.ApiGuidelines.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Core.Common;
    using Domain.Owner;
    using Domain.Repositories;

    public class OwnerRepository : IOwnerRepository
    {
        public Task<Maybe<Owner>> FindById(Guid id)
        {
            return Task.FromResult(InMemoryEntities.Owners.ContainsKey(id)
                ? new Maybe<Owner>(InMemoryEntities.Owners[id])
                : new Maybe<Owner>());
        }
    }
}
