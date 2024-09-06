namespace Article.Domain.Abstractions;

public abstract class Entity(Guid id)
{
    public Guid Id { get; init; } = id;
    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }
    public bool IsAccepted { get; private set; }
    public bool IsDeleted { get; private set; }

    public void Accept(bool isAccepted = true)
    {
        IsAccepted = isAccepted;
    }

    public void Delete(bool isDeleted = true)
    {
        IsDeleted = isDeleted;
    }

    public void CreateAt(DateTime? dateTime = null)
    {
        if (dateTime is not null)
        {
            CreatedAt = (DateTime)dateTime;
            ModifiedAt = (DateTime)dateTime;
        }

        var now = DateTime.Now;
        CreatedAt = now;
        ModifiedAt = now;
    }

    public void ModifyAt(DateTime? dateTime = null)
    {
        if (dateTime is not null)
        {
            ModifiedAt = (DateTime)dateTime;
        }

        ModifiedAt = DateTime.Now;
    }
}