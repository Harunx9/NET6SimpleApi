namespace SimpleApi.Model.Base;

public interface IVersionedEntity
{
    long Version { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime ModifiedAt { get; set; }
    bool IsDeleted { get; }
}