namespace CT.ApiGuidelines.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Common;
    using Domain.Account;
    using Domain.Repositories;

    public class AccountRepository : IAccountRepository
    {
        private static readonly List<Action> AddUpdateAndDeleteQueue = new List<Action>();

        public Task<IEnumerable<Account>> FindAll()
        {
            return Task.FromResult(InMemoryEntities.Accounts.Values.AsEnumerable());
        }

        public Task<Maybe<Account>> FindById(Guid id)
        {
            return Task.FromResult(InMemoryEntities.Accounts.ContainsKey(id)
                                       ? new Maybe<Account>(InMemoryEntities.Accounts[id])
                                       : new Maybe<Account>());
        }

        public void Add(Account account)
        {
            AddUpdateAndDeleteQueue.Add(() => InMemoryEntities.Accounts.Add(account.Id, account));
        }

        public void Update(Account account)
        {
            AddUpdateAndDeleteQueue.Add(() => InMemoryEntities.Accounts[account.Id] = account);
        }

        public void Delete(Account account)
        {
            AddUpdateAndDeleteQueue.Add(() => InMemoryEntities.Accounts.Remove(account.Id));
            InMemoryEntities.Accounts.Remove(account.Id);
        }

        public Task SaveChanges()
        {
            AddUpdateAndDeleteQueue.ForEach(action => action());
            AddUpdateAndDeleteQueue.Clear();
            return Task.CompletedTask;
        }
    }
}
