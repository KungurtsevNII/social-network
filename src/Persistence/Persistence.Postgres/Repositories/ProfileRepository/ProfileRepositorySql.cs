namespace Persistence.Postgres.Repositories.ProfileRepository;

internal static class ProfileRepositorySql
{
    public const string FindByFirstAndLastNormalizedName = @"
        SELECT user_id, first_name, last_name, middle_name, age, sex, interests, city
        FROM profiles
        WHERE LOWER(last_name) LIKE @lastName
        AND LOWER(first_name) LIKE @firstName;";
}