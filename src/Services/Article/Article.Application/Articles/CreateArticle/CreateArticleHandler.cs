using Article.Application.Abstractions;
using Article.Domain.Articles;

namespace Article.Application.Articles.CreateArticle;

public class CreateArticleHandler(
    IArticleRepository articleRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateArticleCommand, CreateArticleResult>
{
    public async Task<CreateArticleResult> Handle(
        CreateArticleCommand command,
        CancellationToken cancellationToken)
    {
        var article = ArticleEntity.Create(
            command.CategoryId,
            command.AuthorId,
            command.ImageName,
            command.Title,
            command.Text,
            new Tag(command.Tags));

        // TODO: Check result

        articleRepository.Create(article);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateArticleResult();
    }
}