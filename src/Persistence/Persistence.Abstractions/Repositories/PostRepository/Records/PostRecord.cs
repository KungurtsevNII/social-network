namespace Persistence.Abstractions.Repositories.PostRepository.Records;

public sealed class PostRecord
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
}