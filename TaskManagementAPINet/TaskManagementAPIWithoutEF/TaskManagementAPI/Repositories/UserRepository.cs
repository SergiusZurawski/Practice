

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
}