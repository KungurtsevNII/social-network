namespace Persistence.Postgres.Repositories.UserRepository;

internal static class UserRepositorySql
{
    internal const string FindByNormalizedSql = @"
        SELECT id, email, normalized_email, email_confirmed,
               password_hash, phone_number, phone_number_confirmed, two_factor_enabled
        FROM users
        WHERE normalized_email = @normalizedEmail;";

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
        ON CONFLICT (id) DO UPDATE SET 
            email = EXCLUDED.email,
            normalized_email = EXCLUDED.normalized_email,
            email_confirmed = EXCLUDED.email_confirmed,
            password_hash = EXCLUDED.password_hash,
            phone_number = EXCLUDED.phone_number,
            phone_number_confirmed = EXCLUDED.phone_number_confirmed,
            two_factor_enabled = EXCLUDED.two_factor_enabled;";
}