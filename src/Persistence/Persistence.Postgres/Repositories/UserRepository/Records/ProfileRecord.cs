using Domain.UserAggregate;

namespace Persistence.Postgres.Repositories.UserRepository.Records;

public sealed class ProfileRecord
{
    public long UserId { get; set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public int Age { get; private set; }
    public Sex Sex { get; private set; }
    public string? Interests { get; private set; }
    public string City { get; private set; }
}