namespace Article.Domain.Articles;

public record Tag
{
    public static readonly char Separator = ',';

    public Tag(IEnumerable<string>? tags)
    {
        if (tags != null)
            Value = string.Join(Separator, tags);

        Value = string.Empty;
    }

    public Tag(string tags)
        => Value = tags;

    public IEnumerable<string> FromString() 
        => Value.Split(Separator).AsEnumerable();

    public string Value { get; }
}