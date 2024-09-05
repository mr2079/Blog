using MongoDB.Bson;
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
    [BsonElement("replies")]
    public ICollection<Comment>? Replies { get; private set; }

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

    public void AddReply(Comment reply)
    {
        var replies = Replies?.ToList() ?? new List<Comment>();
        replies.Add(reply);
        Replies = replies;
    }

    public void RemoveReply(Guid id)
    {
        var replies = Replies?.ToList();
        var reply = replies?.SingleOrDefault(c => c.Id == id);
        if (reply is null) return;
        replies?.Remove(reply);
        Replies = replies;
    }
}