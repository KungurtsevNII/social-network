using Domain.UserAggregate;

namespace Application.Dtos;

public sealed record ProfileDto(
    string FirstName,
    string LastName,
    string? MiddleName,
    int Age,
    Sex Sex,
    string? Interests,
    string City);