using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Hubs;

public class CustomUserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims
            ?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value 
               ?? throw new InvalidOperationException();
    }
}