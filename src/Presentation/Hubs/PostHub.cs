using Microsoft.AspNetCore.SignalR;

namespace Hubs;

public sealed class PostHub : Hub
{
    public async Task SendMessage(string to, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", to, message);
    }
}