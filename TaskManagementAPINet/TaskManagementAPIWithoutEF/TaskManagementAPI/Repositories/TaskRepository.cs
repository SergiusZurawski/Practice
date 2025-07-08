using Dapper;
using Npgsql;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    // Repositories/TaskRepository.cs

    public class TaskRepository : ITaskRepository
    {
        private readonly IConfiguration _configuration;

        public TaskRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM tasks ORDER BY id";
            return await connection.QueryAsync<TaskItem>(sql);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksForUserAsync(int userId)
        { 
            using var connection = CreateConnection();
            var sql = "SELECT * FROM tasks WHERE userid = @UserId ORDER BY id";
            return await connection.QueryAsync<TaskItem>(sql, new { UserId = userId });
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM tasks WHERE id = @Id";
            Console.WriteLine(connection.ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<TaskItem>(sql, new { Id = id });
        }

        public async Task<int> CreateAsync(TaskItem task)
        {
            using var connection = CreateConnection();
            var sql = @"
                INSERT INTO tasks (title, description, iscompleted, createdat)
                VALUES (@Title, @Description, @IsCompleted, @CreatedAt)
                RETURNING id";
            task.CreatedAt = DateTime.UtcNow;
            return await connection.ExecuteScalarAsync<int>(sql, task);
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            using var connection = CreateConnection();
            var sql = @"
                UPDATE tasks
                SET title = @Title,
                    description = @Description,
                    iscompleted = @IsCompleted
                WHERE id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, task);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM tasks WHERE id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }

}