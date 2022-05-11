namespace Persistence.Postgres.Repositories.PostRepository;

public static class PostRepositorySql
{
    internal const string SaveSql = @"
        INSERT INTO posts (
            id,
            user_id,
            text)
        VALUES(
            @id,
            @userId,
            @text)
        ON CONFLICT (id, user_id) DO UPDATE SET 
            text = EXCLUDED.text;
    ";
}