namespace Application.Services;

public interface IPasswordHasherService
{
    string ComputeHash(string password);
}