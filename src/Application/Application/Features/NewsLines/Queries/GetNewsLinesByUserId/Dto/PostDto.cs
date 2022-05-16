namespace Application.Features.NewsLines.Queries.GetNewsLinesByUserId.Dto;

public sealed record PostDto(long PostedUserId, string Text);