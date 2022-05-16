using Application.Features.NewsLines.Queries.GetNewsLinesByUserId.Dto;

namespace Application.Features.NewsLines.Queries.GetNewsLinesByUserId;

public sealed record GetNewsLinesByUserIdQueryResult(IReadOnlyList<PostDto> Posts);