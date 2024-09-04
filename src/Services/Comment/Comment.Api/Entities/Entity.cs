using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Comment.Api.Entities;

public abstract class Entity(Guid id)
{
    public Guid Id { get; init; } = id;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; private set; }

    [BsonElement("updated_at")]
    public DateTime UpdatedAt { get; private set; }

    [BsonElement("is_accepted")]
    public bool IsAccepted { get; private set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; private set; }

    public void Accept(bool isAccepted)
    {
        IsAccepted = isAccepted;
    }

    public void Delete(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }

    public void CreateAt(DateTime? dateTime = null)
    {
        if (dateTime is not null)
        {
            CreatedAt = (DateTime)dateTime;
            UpdatedAt = (DateTime)dateTime;
        }

        var now = DateTime.Now;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public void UpdateAt(DateTime? dateTime = null)
    {
        if (dateTime is not null)
        {
            UpdatedAt = (DateTime)dateTime;
        }

        UpdatedAt = DateTime.Now;
    }
}