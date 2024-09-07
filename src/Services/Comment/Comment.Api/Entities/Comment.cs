using MongoDB.Bson.Serialization.Attributes;

namespace Comment.Api.Entities;

public sealed class Comment : Entity
{
    private Comment(
        Guid id,
        string userId,
        string articleId,
        string text,
        Guid? parentId = null) : base(id)
    {
        UserId = userId;
        ArticleId = articleId;
        Text = text;
        ParentId = parentId;
    }

    [BsonElement("user_id")]
    public string UserId { get; private set; }
    [BsonElement("article_id")]
    public string ArticleId { get; private set; }
    [BsonElement("parent_id")]
    public Guid? ParentId { get; private set; }
    [BsonElement("text")]
    public string Text { get; private set; }
    [BsonElement("reply_ids")]
    public ICollection<Guid>? ReplyIds { get; private set; }

    public static Comment Create(
        object userId,
        object articleId,
        string text,
        Guid? parentId = null)
    {
        return new Comment(
            Guid.NewGuid(),
            userId.ToString()!,
            articleId.ToString()!,
            text,
            parentId);
    }

    public void Update(
        string text)
    {
        Text = text;
    }

    public void AddReply(Guid id)
    {
        var replyIds = ReplyIds?.ToList() ?? new List<Guid>();
        replyIds.Add(id);
        ReplyIds = replyIds;
    }

    public void RemoveReply(Guid id)
    {
        var replyIds = ReplyIds?.ToList();
        replyIds?.Remove(id);
        ReplyIds = replyIds;
    }
}