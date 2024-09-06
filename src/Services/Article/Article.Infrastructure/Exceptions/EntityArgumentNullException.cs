namespace Article.Infrastructure.Exceptions;

public class EntityArgumentNullException<TEntity>() 
    : ArgumentNullException($"{typeof(TEntity).Name}.Null", $"Entered {typeof(TEntity).Name} is null!")
{
    public static void ThrowIfNull(TEntity? argument)
    {
        if (argument is not null)
            return;

        throw new EntityArgumentNullException<TEntity>();
    }
}