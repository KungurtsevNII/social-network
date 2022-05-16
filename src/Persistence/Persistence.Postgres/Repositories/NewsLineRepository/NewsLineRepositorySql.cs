namespace Persistence.Postgres.Repositories.NewsLineRepository;

public static class NewsLineRepositorySql
{
    internal const string SaveSql = @"
        INSERT INTO news_lines (
            id,
            post_id,
            post_creater_user_id,
            news_line_owner_user_id)
        VALUES(
           @id,
           @postId,
           @postCreaterUserId,
           @newsLineOwnerUserId)
        ON CONFLICT (id) DO NOTHING;
    ";
    
    internal const string GetNewsLinesBynNewsLineOwnerUserIdSql = @"
        SELECT 
            id,
            post_id,
            news_line_owner_user_id,
            post_creater_user_id
        FROM news_lines
        WHERE news_line_owner_user_id = @newsLineOwnerUserId
    ";
}