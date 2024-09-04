using MongoDB.Bson;

namespace Comment.Api.Exceptions;

public class GuidArgumentException()
    : ArgumentNullException($"ObjectId.NullOrEmpty", "Entered 'Guid' is null or empty!")
{
    public static void ThrowIfNull(Guid? argument)
    {
        if (argument != null && argument != Guid.Empty)
            return;

        throw new GuidArgumentException();
    }
}