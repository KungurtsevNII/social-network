using MediatR;

namespace Application.Features.Users.Queries.GetProfileListByName;

public sealed record GetProfileListByNameQuery(
    string FirstName,
    string LastName) : IRequest<GetProfileListByNameQueryResult>;