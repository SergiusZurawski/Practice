namespace TaskManagementAPI.Services;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(string username, string password);
}
