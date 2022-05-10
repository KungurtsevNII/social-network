using MediatR;

namespace Application.Features.Posts.Command.CreatePost;

public sealed record CreatePostCommand(
    long UserId,
    string PostText) : IRequest;