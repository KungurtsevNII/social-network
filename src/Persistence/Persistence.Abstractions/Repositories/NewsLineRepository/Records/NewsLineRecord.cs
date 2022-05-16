namespace Persistence.Abstractions.Repositories.NewsLineRepository.Records;

public sealed class NewsLineRecord
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public long NewsLineOwnerUserId { get; set; }
    public long PostCreaterUserId { get; set; }
}