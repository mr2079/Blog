using Article.Domain.Abstractions;

namespace Article.Domain.Categories;

public sealed class Category : Entity
{
    private Category(
        Guid id,
        string title) : base(id)
    {
        Title = title;
    }

    public string Title { get; private set; }

    public static Category Create(
        string title)
    {
        return new Category(
            Guid.NewGuid(),
            title);
    }
}