using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Comment.Api.Entities;

public abstract class Entity(ObjectId id)
{
    public ObjectId Id { get; init; } = id;

    [BsonElement("created_at")]
    [BsonRepresentation(BsonType.Timestamp)]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updated_at")]
    [BsonRepresentation(BsonType.Timestamp)]
    public DateTime UpdatedAt { get; set; }

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
}