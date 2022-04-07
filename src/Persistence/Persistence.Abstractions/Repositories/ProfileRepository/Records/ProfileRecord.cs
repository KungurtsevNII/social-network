using Domain.UserAggregate;

namespace Persistence.Abstractions.Repositories.ProfileRepository.Records;

public sealed class ProfileRecord
{
    public long UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string? Interests { get; set; }
    public string City { get; set; } = null!;
}