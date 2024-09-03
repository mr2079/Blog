namespace Comment.Api.Exceptions;

public class PredicateArgumentNullException()
    : ArgumentNullException($"Predicate.Null", "Entered predicate is null!")
{
    public static void ThrowIfNull(object? argument)
    {
        if (argument != null)
            return;

        throw new PredicateArgumentNullException();
    }
}