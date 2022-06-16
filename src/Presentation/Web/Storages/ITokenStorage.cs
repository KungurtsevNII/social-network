namespace Web.Storages;

public interface ITokenStorage
{
    Task<bool> Exists(CancellationToken ct);

    Task<string?> Get(CancellationToken ct);

    Task Save(string token, CancellationToken ct);

    Task Remove(CancellationToken ct);
}