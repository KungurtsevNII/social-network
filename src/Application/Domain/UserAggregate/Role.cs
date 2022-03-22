using SharedKernel;

namespace Domain.UserAggregate;

public sealed class Role : Entity<long>
{
    public string Name { get; private set; }
    public string NormalizedName { get; private set; }

    public Role(long id, string name, string normalizedName) : base(id)
    {
        Name = name;
        NormalizedName = normalizedName;
    }
}