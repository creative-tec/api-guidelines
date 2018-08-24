namespace CT.ApiGuidelines.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Account;
    using Core.Common;

    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> FindAll();

        Task<Maybe<Account>> FindById(Guid id);

        void Add(Account account);

        void Update(Account account);

        void Delete(Account account);

        Task SaveChanges();
    }
}
