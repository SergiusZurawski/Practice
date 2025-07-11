// Services/TaskService.cs
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TaskItem>> GetAllTasksAsync() => _repository.GetAllAsync();

        public Task<IEnumerable<TaskItem>> GetTasksForUserAsync(int userId) => _repository.GetTasksForUserAsync(userId);

        public Task<TaskItem?> GetTaskByIdAsync(int id) => _repository.GetByIdAsync(id);
        
        public async Task<int> CreateTaskAsync(TaskItem task)
        {
            // Ensure TaskStatusId is always set to "Open" if not provided
            if (task.TaskStatusId == null || task.TaskStatusId == 0)
            {
                task.TaskStatusId = 1; // 1 = "Open"
            }
            
            if (task.CreatedAt == default)
            {
                task.CreatedAt = DateTime.Now;
            }

            return await _repository.CreateAsync(task);
        }

        public Task<bool> UpdateTaskAsync(TaskItem task) => _repository.UpdateAsync(task);

        public Task<bool> DeleteTaskAsync(int id) => _repository.DeleteAsync(id);
    }
}