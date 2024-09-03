namespace Comment.Api.Exceptions;

public class CommentArgumentNullException()
    : ArgumentNullException($"Comment.Null", "Entered Comment is null!")
{
    public static void ThrowIfNull(CommentEntity? argument)
    {
        if (argument is not null)
            return;

        throw new CommentArgumentNullException();
    }
}