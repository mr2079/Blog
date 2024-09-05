namespace Comment.Api.DTOs;

public record CommentDto(
    Guid Id,
    string UserId,
    string ArticleId,
    string Text,
    IReadOnlyList<CommentDto>? Replies
);