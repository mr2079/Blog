using MongoDB.Bson;

namespace Comment.Api.Exceptions;

public class ObjectIdArgumentException()
    : ArgumentNullException($"ObjectId.NullOrEmpty", "Entered 'ObjectId' is null or empty!")
{
    public static void ThrowIfNull(ObjectId? argument)
    {
        if (argument != null && argument != ObjectId.Empty)
            return;

        throw new ObjectIdArgumentException();
    }
}