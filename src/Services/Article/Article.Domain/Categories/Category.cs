using Article.Domain.Abstractions;
using ArticleEntity = Article.Domain.Articles.Article;

namespace Article.Domain.Categories;

public class Category : Entity
{
    private Category(
        Guid id,
        string title) : base(id)
    {
        Title = title;
    }

    public string Title { get; private set; }

    public virtual ICollection<ArticleEntity>? Articles { get; set; }

    public static Category Create(
        string title)
    {
        return new Category(
            Guid.NewGuid(),
            title);
    }
}