

using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto?> GetByUsernameAsync(string username);
    }
}