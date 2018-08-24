namespace CT.ApiGuidelines.Domain.Owner
{
    using System;
    using Core.Common;
    using EnsureThat;

    public sealed class Owner : RootEntity<Guid>
    {
        public Owner(Guid id, string firstName, string lastName)
        {
            Id = Ensure.Guid.IsNotEmpty(id, nameof(id));
            FirstName = Ensure.String.IsNotNullOrWhiteSpace(firstName, nameof(firstName));
            LastName = Ensure.String.IsNotNullOrWhiteSpace(lastName, nameof(lastName));
        }

        public string FirstName { get; }

        public string LastName { get; }
    }
}
