using SimpleApi.Model.Base;

namespace SimpleApi.Model;

public class TaskEntity : Entity<Guid>
{
    public string Title { get; set; }
    public DateTime? DueDate { get; set; }

    public string Desctiption { get; set; }

    public TaskEntity(string title, DateTime? dueDate, string desctiption)
        : this(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, title, dueDate, desctiption)
    {
    }

    protected TaskEntity(Guid id, string title, DateTime? dueDate, string desctiption) : base(id)
    {
        Title = title;
        DueDate = dueDate;
        Desctiption = desctiption;
    }

    protected TaskEntity(Guid id, DateTime createdAt, DateTime modifiedAt, string title, DateTime? dueDate,
        string desctiption, bool isDeleted = false) : base(id, createdAt, modifiedAt, isDeleted)
    {
        Title = title;
        DueDate = dueDate;
        Desctiption = desctiption;
    }
}