namespace Comment.Api.Errors;

public static class CommentErrors
{
    public static Error NotFound
        => new("Comments.Found", "There is no comment with this information!");

    public static Error NotUpdated
        => new("Comments.Update", "The comment update operation failed!");

    public static Error NotDeleted
        => new("Comments.Delete", "The comment delete operation failed!");
}