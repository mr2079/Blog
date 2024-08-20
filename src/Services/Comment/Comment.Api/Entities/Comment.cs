using MongoDB.Bson;

namespace Comment.Api.Entities;

public sealed class Comment : Entity
{
    private Comment(
        ObjectId id,
        string userId,
        string articleId,
        string text,
        ObjectId? parentId = null) : base(id)
    {
        UserId = userId;
        ArticleId = articleId;
        Text = text;
        ParentId = parentId;
    }

    public string UserId { get; private set; }
    public string ArticleId { get; private set; }
    public ObjectId? ParentId { get; private set; }
    public string Text { get; private set; }
    public ICollection<Comment>? Replies { get; private set; }

    public static Comment Create(
        object userId,
        object articleId,
        string text,
        ObjectId? parentId = null)
    {
        return new Comment(
            ObjectId.GenerateNewId(),
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

    public void RemoveReply(ObjectId id)
    {
        var replies = Replies?.ToList();
        var reply = replies?.SingleOrDefault(c => Equals(c.Id, id));
        if (reply is null) return;
        replies?.Remove(reply);
        Replies = replies;
    }
}