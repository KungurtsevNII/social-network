using System.Security.Cryptography;
using System.Text;
using Application.Services;

namespace Services.Auth;

public sealed class PasswordHasherService : IPasswordHasherService
{
    public string ComputeHash(string password)
    {
        using var md5 = MD5.Create();
        var result = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Encoding.UTF8.GetString(result);
    }
}