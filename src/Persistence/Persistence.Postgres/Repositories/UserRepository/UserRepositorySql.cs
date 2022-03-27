namespace Persistence.Postgres.Repositories.UserRepository;

internal static class UserRepositorySql
{
    internal const string FindByNormalizedSql = @"
        SELECT id, email, normalized_email, email_confirmed,
               password_hash, phone_number, phone_number_confirmed, two_factor_enabled
        FROM users
        WHERE normalized_email = @normalizedEmail;";
    
    internal const string FindFriendsIdsSql = @"
        SELECT friend_id
        FROM friends
        WHERE user_id = @userId;";
    
    internal const string FindByIdSql = @"
        SELECT id, email, normalized_email, email_confirmed,
               password_hash, phone_number, phone_number_confirmed, two_factor_enabled
        FROM users
        WHERE id = @id;";

    internal const string SaveSql = @"
        INSERT INTO users (
            email,
            normalized_email,
            email_confirmed,
            password_hash,
            phone_number,
            phone_number_confirmed,
            two_factor_enabled)
        VALUES(
            @email,
            @normalizedEmail,
            @emailConfirmed,
            @passwordHash,
            @phoneNumber,
            @phoneNumberConfirmed,
            @twoFactorEnabled)
        ON CONFLICT (normalized_email, phone_number) DO UPDATE SET 
            email = EXCLUDED.email,
            normalized_email = EXCLUDED.normalized_email,
            email_confirmed = EXCLUDED.email_confirmed,
            password_hash = EXCLUDED.password_hash,
            phone_number = EXCLUDED.phone_number,
            phone_number_confirmed = EXCLUDED.phone_number_confirmed,
            two_factor_enabled = EXCLUDED.two_factor_enabled;";
    
    internal const string SaveFriendsSql = @"
        INSERT INTO friends (
            user_id,
            friend_id)
        VALUES(
            @userId,
            @friendId)
        ON CONFLICT DO NOTHING;";
}