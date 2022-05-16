using Domain.PostAggregate.Events;
using SharedKernel;

namespace Domain.PostAggregate;

public sealed class Post : AggregateRoot<Guid>
{
    public long UserId { get; }
    public string Text { get; private set; }
    public DateTime CreatedAt { get; }
    
    public Post(Guid id, long userId, string text, DateTime createdAt) : base(id)
    {
        UserId = userId;
        Text = text;
        CreatedAt = createdAt;
    }

    public static Post Create(long userId, string text)
    {
        var post = new Post(Guid.NewGuid(), userId, text, DateTime.UtcNow);
        post.AddDomainEvent(new PostCreatedDomainEvent(post.Id, post.UserId, post.Text));
        return post;
    } 
}