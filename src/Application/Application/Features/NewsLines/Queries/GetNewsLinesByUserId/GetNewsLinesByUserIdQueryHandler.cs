using Application.Features.NewsLines.Queries.GetNewsLinesByUserId.Dto;
using Application.Options;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Abstractions.Repositories.NewsLineRepository;
using Persistence.Abstractions.Repositories.PostRepository;
using Persistence.Abstractions.Repositories.UserRepository;

namespace Application.Features.NewsLines.Queries.GetNewsLinesByUserId;

public sealed class GetNewsLinesByUserIdQueryHandler : IRequestHandler<GetNewsLinesByUserIdQuery, GetNewsLinesByUserIdQueryResult>
{
    private readonly INewsLineRepository _newsLineRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly CelebrityOptions _celebrityOptions;

    public GetNewsLinesByUserIdQueryHandler(
        INewsLineRepository newsLineRepository,
        IPostRepository postRepository, 
        IUserRepository userRepository, 
        IOptions<CelebrityOptions> celebrityOptions)
    {
        _newsLineRepository = newsLineRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
        _celebrityOptions = celebrityOptions.Value;
    }

    public async Task<GetNewsLinesByUserIdQueryResult> Handle(
        GetNewsLinesByUserIdQuery query,
        CancellationToken ct)
    {
        var newsLines = await _newsLineRepository.GetNewsLinesBynNewsLineOwnerUserId(query.UserId, ct);
        var postsFromNewsLine = await _postRepository.GetPostsByIds(newsLines.Select(x => x.PostId).ToList(), ct);

        var celebritiesPosts = await _postRepository.GetPostsByUsersIds(new List<long> { 100006 }, ct);
        
        return new GetNewsLinesByUserIdQueryResult(
            postsFromNewsLine
                .Union(celebritiesPosts)
                .Select(x => new PostDto(x.UserId, x.Text)).ToList());
    }
}