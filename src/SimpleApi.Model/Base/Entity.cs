namespace SimpleApi.Model.Base;

public class Entity<TIdentity> : IVersionedEntity
{
    public TIdentity Id { get; set; }
    public long Version { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsDeleted { get; protected set; }

    protected Entity(TIdentity id)
    {
        Id = id;
        Version = 1;
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
        IsDeleted = false;
    }
        
    protected Entity(TIdentity id, DateTime createdAt, DateTime modifiedAt, bool isDeleted = false)
    {
        Id = id;
        Version = 1;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        IsDeleted = isDeleted;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}