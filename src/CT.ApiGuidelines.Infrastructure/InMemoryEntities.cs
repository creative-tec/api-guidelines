namespace CT.ApiGuidelines.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Account;
    using Domain.Owner;

    internal static class InMemoryEntities
    {
        internal static readonly Dictionary<Guid, Owner> Owners = SeedOwners();
        internal static readonly Dictionary<Guid, Account> Accounts = SeedAccounts();

        private static Dictionary<Guid, Owner> SeedOwners()
        {
            var owner1 = new Owner(new Guid("11111111-0000-0000-0000-000000000001"), "Jim", "Ratcliffe");
            var owner2 = new Owner(new Guid("11111111-0000-0000-0000-000000000002"), "Sri", "Hinduja");
            var owner3 = new Owner(new Guid("11111111-0000-0000-0000-000000000003"), "Leonard", "Bavatnik");

            return new Dictionary<Guid, Owner>
            {
                { owner1.Id, owner1 },
                { owner2.Id, owner2 },
                { owner3.Id, owner3 },
            };
        }

        private static Dictionary<Guid, Account> SeedAccounts()
        {
            var account1 = new Account(
                new Guid("00000000-0000-0000-0000-000000000001"),
                new AccountReference("FW", 1),
                Owners.First().Key);

            account1.Deposit(10);
            account1.Deposit(20);
            account1.Deposit(30);
            account1.Deposit(40);
            account1.Deposit(50);
            account1.Deposit(60);
            account1.Withdraw(5);
            account1.Deposit(70);
            account1.Deposit(80);
            account1.Deposit(90);

            var account2 = new Account(
                new Guid("00000000-0000-0000-0000-000000000002"),
                new AccountReference("FW", 2),
                Owners.Skip(1).First().Key);

            account2.Deposit(100);
            account2.Deposit(200);
            account2.Deposit(300);

            var account3 = new Account(
                new Guid("00000000-0000-0000-0000-000000000003"),
                new AccountReference("FW", 3),
                Owners.Skip(2).First().Key);

            return new Dictionary<Guid, Account>
            {
                { account1.Id, account1 },
                { account2.Id, account2 },
                { account3.Id, account3 },
            };
        }
    }
}
