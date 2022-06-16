using Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Persistence.Abstractions.Repositories.PostRepository;
using Persistence.Abstractions.Repositories.UserRepository;

namespace Application.Features.NewsLines.Command.AddPostInNewLineOnline;

public sealed class AddPostInNewLineOnlineCommandHandler
    : IRequestHandler<AddPostInNewLineOnlineCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly IHubContext<PostHub> _hubContext;

    public AddPostInNewLineOnlineCommandHandler(
        IUserRepository userRepository, 
        IPostRepository postRepository, IHubContext<PostHub> hubContext)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
        _hubContext = hubContext;
    }

    public async Task<Unit> Handle(AddPostInNewLineOnlineCommand command, CancellationToken ct)
    {
        var friendsIds = await _userRepository.GetUserFriends(command.UserId, ct);
        var friends = friendsIds.Select(x => x.ToString()).ToList();
        var post = await _postRepository.GetPostById(command.PostId, ct);
        await _hubContext
            .Clients
            .Users(friends)
            .SendAsync("ReceiveMessage", post.UserId.ToString(), $"{post.Text} - {post.CreatedAt}", ct);
        return Unit.Value;
    }
}
