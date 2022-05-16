using SharedKernel;

namespace Domain.NewsLineAggregate;

public sealed class NewsLine : AggregateRoot<Guid>
{
    public long NewsLineOwnerUserId { get; }
    public long PostCreaterUserId { get; }

    public Guid PostId { get; }
    
    public NewsLine(
        Guid id,
        Guid postId,
        long postCreaterUserId, 
        long newsLineOwnerUserId) : base(id)
    {
        PostId = postId;
        PostCreaterUserId = postCreaterUserId;
        NewsLineOwnerUserId = newsLineOwnerUserId;
    }

    public static NewsLine Create(
        Guid postId,
        long postCreaterUserId,
        long newsLineOwnerUserId)
    {
        return new NewsLine(
            Guid.NewGuid(),
            postId,
            postCreaterUserId, 
            newsLineOwnerUserId);
    }
}