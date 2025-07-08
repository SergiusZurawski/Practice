// Repositories/ITaskRepository.cs
using TaskManagementAPI.Models;
namespace TaskManagementAPI.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> GetTasksForUserAsync(int userId);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<int> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
    }
}
