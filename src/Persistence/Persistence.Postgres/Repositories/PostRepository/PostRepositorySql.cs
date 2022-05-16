namespace Persistence.Postgres.Repositories.PostRepository;

public static class PostRepositorySql
{
    internal const string SaveSql = @"
        INSERT INTO posts (
            id,
            user_id,
            text,
            created_at)
        VALUES(
            @id,
            @userId,
            @text,
            @createdAt)
        ON CONFLICT (id, user_id) DO UPDATE SET 
            text = EXCLUDED.text;
    ";
    
    internal const string GetPostsByIdsSql = @"
        SELECT 
            id,
            user_id,
            text,
            created_at
        FROM posts
        WHERE id = ANY(@ids);
    ";
    
    internal const string GetCelebrityPostsSql = @"
        SELECT 
            id,
            user_id,
            text,
            created_at
        FROM posts
        WHERE user_id = ANY(@usersIds);
    ";
}