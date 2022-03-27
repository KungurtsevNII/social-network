using SharedKernel;

namespace Domain.UserAggregate;

public sealed class Profile : Entity<long>
{
    public Profile(long id) : base(id)
    {
    }
}