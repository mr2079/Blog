using Article.Domain.Abstractions;

namespace Article.Domain.Articles;

public class Article : Entity
{
    private Article(
        Guid id,
        Guid categoryId,
        string authorId,
        string imageName,
        string title,
        string text,
        Tag? tag = null,
        int view = 0) : base(id)
    {
        CategoryId = categoryId;
        AuthorId = authorId;
        ImageName = imageName;
        Title = title;
        Text = text;
        Tag = tag;
        View = view;
    }

    public Guid CategoryId { get; private set; }
    public string AuthorId { get; private set; }
    public string ImageName { get; private set; }
    public string Title { get; private set; }
    public string Text { get; private set; }
    public Tag? Tag { get; private set; }
    public int View { get; private set; }

    public virtual CategoryEntity? Category { get; set; }

    public static Article Create(
        Guid categoryId,
        string authorId,
        string imageName,
        string title,
        string text,
        Tag? tag = null,
        int view = 0)
    {
        return new Article(
            Guid.NewGuid(),
            categoryId,
            authorId,
            imageName,
            title,
            text,
            tag,
            view);
    }
}