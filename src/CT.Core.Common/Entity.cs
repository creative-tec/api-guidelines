namespace CT.Core.Common
{
    public abstract class Entity<TEntityId>
        where TEntityId : struct
    {
        public virtual TEntityId Id { get; protected internal set; }

        public static bool operator ==(Entity<TEntityId> a, Entity<TEntityId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TEntityId> a, Entity<TEntityId> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<TEntityId>;

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.GetHashCode();
        }

        protected bool Equals(Entity<TEntityId> other)
        {
            return Equals((object)other);
        }
    }
}
