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
    
    public Profile Profile { get; private set; }
    
    private List<Role> _roles;
    public IReadOnlyList<Role> Roles => _roles;
    
    private List<long> _friends;
    public IReadOnlyList<long> Friends => _friends;

    public User(
        long id, 
        string email,
        string normalizedEmail, 
        bool emailConfirmed, 
        string passwordHash, 
        string? phoneNumber,
        bool phoneNumberConfirmed,
        bool twoFactorEnabled,
        List<Role> userRoles, 
        List<long> friends, 
        Profile profile) : base(id)
    {
        Email = email;
        NormalizedEmail = normalizedEmail;
        EmailConfirmed = emailConfirmed;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        TwoFactorEnabled = twoFactorEnabled;
        _roles = userRoles;
        _friends = friends;
        Profile = profile;
    }
    
    private User(
        string email,
        string normalizedEmail, 
        bool emailConfirmed, 
        string passwordHash, 
        string? phoneNumber,
        bool phoneNumberConfirmed,
        bool twoFactorEnabled,
        List<Role> userRoles,
        List<long> friends, 
        Profile profile) : base(0)
    {
        Email = email;
        NormalizedEmail = normalizedEmail;
        EmailConfirmed = emailConfirmed;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        TwoFactorEnabled = twoFactorEnabled;
        _roles = userRoles;
        _friends = friends;
        Profile = profile;
    }

    public static User Create(
        string email,
        string passwordHash, 
        string? phoneNumber,
        bool twoFactorEnabled,
        List<Role> userRoles)
    {
        return new User(
            email,
            email.ToLower(),
            false,
            passwordHash,
            phoneNumber,
            false,
            twoFactorEnabled,
            userRoles,
            new List<long>(),
            new Profile(default, default, default, default, default, default,default, default));
    }

    public void AddFriend(long userFriendId)
    {
        if (userFriendId == Id)
        {
            return;
        }
        
        if (_friends.Exists(x => x == userFriendId))
        {
            return;
        }
        _friends.Add(userFriendId);
    }
}