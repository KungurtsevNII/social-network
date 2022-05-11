using Domain.PostAggregate;
using MediatR;
using Persistence.Abstractions.Repositories.PostRepository;
using SharedKernel;

namespace Application.Features.Posts.Command.CreatePost;

public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly IEventBus _eventBus;

    public CreatePostCommandHandler(IPostRepository postRepository, IEventBus eventBus)
    {
        _postRepository = postRepository;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(CreatePostCommand command, CancellationToken ct)
    {
        var newPost = Post.Create(command.UserId, command.PostText);
        await _postRepository.SaveAsync(newPost, ct);
        await _eventBus.PublishAllAsync(newPost.DomainEvents, ct);
        return Unit.Value;
    }
}