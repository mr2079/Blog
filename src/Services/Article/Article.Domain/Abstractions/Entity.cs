namespace Article.Domain.Abstractions;

public abstract class Entity(Guid id)
{
    public Guid Id { get; init; } = id;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsAccepted { get; private set; }
    public bool IsDeleted { get; private set; }

    public void Accept(bool isAccepted)
    {
        IsAccepted = isAccepted;
    }

    public void Delete(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }
}