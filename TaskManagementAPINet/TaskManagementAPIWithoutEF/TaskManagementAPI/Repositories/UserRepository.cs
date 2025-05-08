

using TaskManagementAPI.Models;

using Dapper;
using Npgsql;
using System.Data;
namespace TaskManagementAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public async Task<UserDto?> GetByUsernameAsync(string username)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        const string sql = "SELECT id, username, passwordHash, role FROM users WHERE username = @Username";

        return await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new { Username = username });
    }
    public async Task<int> RegisterUserAsync(string username, string passwordHash, string role)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        const string sql = @"
            INSERT INTO users (username, passwordHash, role) 
            VALUES (@Username, @PasswordHash, @Role) 
            RETURNING id;";

        return await connection.ExecuteScalarAsync<int>(sql, new 
        {
            Username = username,
            PasswordHash = passwordHash,
            Role = role
        });
    }
}