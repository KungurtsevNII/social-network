namespace Persistence.Postgres.IdentityStorages;

public sealed class UserRecord
{
    public long Id { get; set; }
    
    public string Email { get; set; } = null!;

    public string NormalizedEmail { get; set; } = null!;

    public bool EmailConfirmed { get; set; }
 
    public string PasswordHash { get; set; } = null!;
 
    public string? PhoneNumber { get; set; }
 
    public bool PhoneNumberConfirmed { get; set; }
 
    public bool TwoFactorEnabled { get; set; }
}