using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Abstractions;

public interface IUserRepository : IUserStore<User>
{
    
}