using MediatR;

namespace Application.Features.NewsLines.Queries.GetNewsLinesByUserId;

public sealed record GetNewsLinesByUserIdQuery(long UserId) : IRequest<GetNewsLinesByUserIdQueryResult>;