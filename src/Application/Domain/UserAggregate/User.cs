using SharedKernel;

namespace Domain.UserAggregate;

public sealed class User : AggregateRoot<long>
{
    public string Email { get; private set; }
 
    public string NormalizedEmail { get; private set; }
 
    public bool EmailConfirmed { get; private set; }
 
    public string PasswordHash { get; private set; }
 
    public string? PhoneNumber { get; private set; }
 
    public bool PhoneNumberConfirmed { get; private set; }
 
    public bool TwoFactorEnabled { get; private set; }
    
    private List<Role> _roles;
    public IReadOnlyList<Role> Roles => _roles;

    public User(
        long id, 
        string email,
        string normalizedEmail, 
        bool emailConfirmed, 
        string passwordHash, 
        string? phoneNumber,
        bool phoneNumberConfirmed,
        bool twoFactorEnabled,
        List<Role> userRoles) : base(id)
    {
        Email = email;
        NormalizedEmail = normalizedEmail;
        EmailConfirmed = emailConfirmed;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        TwoFactorEnabled = twoFactorEnabled;
        _roles = userRoles;
    }
}