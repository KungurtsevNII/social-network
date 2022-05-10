namespace Persistence.Postgres.Repositories.PostRepository;

public static class PostRepositorySql
{
    internal const string SaveSql = @"
        INSERT INTO users (
            id,
            user_id,
            text)
        VALUES(
            @id,
            @user_id,
            @text)
        ON CONFLICT (id, user_id) DO UPDATE SET 
            text = EXCLUDED.text;
    ";
}