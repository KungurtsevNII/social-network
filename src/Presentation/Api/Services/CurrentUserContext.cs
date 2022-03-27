using System.Security.Claims;
using Application.Services;

namespace Api.Services;

public sealed class CurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long UserId
    {
        get
        {
            var idClaim = _httpContextAccessor.HttpContext?.User.Claims
                ?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (idClaim is null)
            {
                throw new ArgumentNullException(nameof(idClaim));
            }

            if (!long.TryParse(idClaim, out var userId))
            {
                throw new ArgumentException("UserId is not long type", nameof(idClaim));
            }
            return userId;
        }
    }
}