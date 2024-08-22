namespace Article.Application.Articles.CreateArticle;

public record CreateArticleCommand(
    Guid CategoryId,
    string AuthorId,
    string ImageName,
    string Title,
    string Text,
    IEnumerable<string>? Tags = null)
    : ICommand<CreateArticleResult>;