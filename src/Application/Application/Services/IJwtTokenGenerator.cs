using Domain.UserAggregate;

namespace Application.Services;

public interface IJwtTokenGenerator
{
    string CreateToken(User user);
}