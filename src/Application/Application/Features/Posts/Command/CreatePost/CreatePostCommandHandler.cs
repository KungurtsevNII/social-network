using Domain.PostAggregate;
using MediatR;
using Persistence.Abstractions.Repositories.PostRepository;

namespace Application.Features.Posts.Command.CreatePost;

public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
{
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(CreatePostCommand command, CancellationToken ct)
    {
        var newPost = Post.Create(command.UserId, command.PostText);
        await _postRepository.SaveAsync(newPost, ct);
        return Unit.Value;
    }
}