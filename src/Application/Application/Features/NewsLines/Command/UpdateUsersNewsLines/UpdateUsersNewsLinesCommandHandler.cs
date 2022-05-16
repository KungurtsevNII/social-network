using Application.Options;
using Domain.NewsLineAggregate;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Abstractions.Repositories.NewsLineRepository;
using Persistence.Abstractions.Repositories.UserRepository;

namespace Application.Features.NewsLines.Command.UpdateUsersNewsLines;

public sealed class UpdateUsersNewsLinesCommandHandler : IRequestHandler<UpdateUsersNewsLinesCommand>
{
    private readonly INewsLineRepository _newsLineRepository;
    private readonly CelebrityOptions _celebrityOptions;
    private readonly IUserRepository _userRepository;

    public UpdateUsersNewsLinesCommandHandler(
        INewsLineRepository newsLineRepository, 
        IOptions<CelebrityOptions> celebrityOptions,
        IUserRepository userRepository)
    {
        _newsLineRepository = newsLineRepository;
        _userRepository = userRepository;
        _celebrityOptions = celebrityOptions.Value;
    }

    public async Task<Unit> Handle(UpdateUsersNewsLinesCommand command, CancellationToken ct)
    {
        var friendCount = await _userRepository.GetFriendsCountAsync(command.UserId, ct);
        if (friendCount > _celebrityOptions.FriendsCount)
        {
            return Unit.Value;
        }

        var friendsIds = await _userRepository.GetUserFriends(command.UserId, ct);
        var newsLines = friendsIds
            .Select(x => NewsLine.Create(command.PostId, command.UserId, x))
            .ToList();
        
        await _newsLineRepository.SaveAsync(newsLines, ct);
        return Unit.Value;
    }
}