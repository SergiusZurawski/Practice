namespace TaskManagementAPI.Services;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(string username, string password);
    Task<int> RegisterUserAsync(string username, string password, string role);
}
