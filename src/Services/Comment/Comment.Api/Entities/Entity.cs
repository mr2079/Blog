using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Comment.Api.Entities;

public abstract class Entity(ObjectId id)
{
    public ObjectId Id { get; set; } = id;

    [BsonElement("created_at")]
    [BsonRepresentation(BsonType.Timestamp)]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updated_at")]
    [BsonRepresentation(BsonType.Timestamp)]
    public DateTime UpdatedAt { get; set; }

    [BsonElement("is_accepted")]
    public bool IsAccepted { get; set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; }
}