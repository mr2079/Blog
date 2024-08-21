namespace Article.Domain.Articles;

public record Tag
{
    public static readonly char Separator = ',';

    public Tag(IEnumerable<string> tags) 
        => Value = string.Join(Separator, tags);

    public static IEnumerable<string> FromString(string tags)
    {
        return tags.Split(Separator).AsEnumerable();
    }

    public string Value { get; set; }
}