

using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<int> RegisterUserAsync(string username, string passwordHash, string role);
    }
}