using System.Text.Json.Serialization;
using MediatR;

namespace Application.Features.Users.Command.AddFriend;

public sealed class AddFriendCommand : IRequest
{
    [JsonIgnore]
    public long CurrentUserId  { get; set; }
    public long NewFriendId  { get; set; }
}