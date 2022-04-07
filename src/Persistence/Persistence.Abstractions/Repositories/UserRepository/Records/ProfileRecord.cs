using Domain.UserAggregate;

namespace Persistence.Abstractions.Repositories.UserRepository.Records;

public sealed class ProfileRecord
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get;  set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string? Interests { get; set; }
    public string City { get; set; }
}