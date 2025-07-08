// Services/ITaskService.cs
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<IEnumerable<TaskItem>> GetTasksForUserAsync(int userId);
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<int> CreateTaskAsync(TaskItem task);
        Task<bool> UpdateTaskAsync(TaskItem task);
        Task<bool> DeleteTaskAsync(int id);
    }
}