namespace CT.Core.Common
{
    public abstract class ChildEntity<TEntityId> : Entity<TEntityId>
        where TEntityId : struct
    {
        public EntityObjectState State { get; protected set; }

        public void MarkAsAdded()
        {
            State = EntityObjectState.Added;
        }

        public void MarkAsDeleted()
        {
            State = EntityObjectState.Deleted;
        }

        public void MarkAsUnchanged()
        {
            State = EntityObjectState.Unchanged;
        }

        public void MarkAsUpdated()
        {
            State = EntityObjectState.Modified;
        }
    }
}
