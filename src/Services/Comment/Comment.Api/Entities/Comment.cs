using MongoDB.Bson;

namespace Comment.Api.Entities;

public sealed class Comment : Entity
{
    private Comment(
        ObjectId id,
        string userId,
        string articleId,
        string text) : base(id)
    {
        UserId = userId;
        ArticleId = articleId;
        Text = text;
    }

    public string UserId { get; set; }
    public string ArticleId { get; set; }
    public string Text { get; set; }
    public ICollection<Comment>? Replies { get; set; }

    public static Comment Create(
        object userId,
        object articleId,
        string text)
    {
        return new Comment(
            ObjectId.GenerateNewId(),
            userId.ToString()!,
            articleId.ToString()!,
            text);
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

    public void AddReply(params Comment[] reply)
    {
        var replies = Replies?.ToList() ?? new List<Comment>();
        replies.AddRange(reply);
        Replies = replies;
    }

    public void AddReply(IEnumerable<Comment> reply)
    {
        var replies = Replies?.ToList() ?? new List<Comment>();
        replies.AddRange(reply);
        Replies = replies;
    }

    public void RemoveReply(Comment reply)
    {
        var replies = Replies?.ToList();
        replies?.Remove(reply);
        Replies = replies;
    }

    public void RemoveReply(ObjectId id)
    {
        var replies = Replies?.ToList();
        var reply = replies?.SingleOrDefault(c => Equals(c.Id, id));
        if (reply is not null)
        {
            RemoveReply(reply);
        } 
    }
}