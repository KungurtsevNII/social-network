using Domain.PostAggregate;
using MediatR;
using Persistence.Abstractions.Repositories.PostRepository;

namespace Application.Features.Posts.Command.UpdateUsersNewsLine;

public sealed class UpdateUsersNewsLineCommandHandler : IRequestHandler<UpdateUsersNewsLineCommand>
{
    private readonly IPostRepository _postRepository;

    public UpdateUsersNewsLineCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(UpdateUsersNewsLineCommand request, CancellationToken cancellationToken)
    {
        await _postRepository.SaveAsync(new Post(request.PostId, request.UserId, "asdasd"), cancellationToken);
        return Unit.Value;
    }
}