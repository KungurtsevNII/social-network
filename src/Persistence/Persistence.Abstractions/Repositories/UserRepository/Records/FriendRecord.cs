namespace Persistence.Abstractions.Repositories.UserRepository.Records;

public sealed class FriendRecord
{
    public long UserId { get; set; }
    public long FriendId { get; set; }
}