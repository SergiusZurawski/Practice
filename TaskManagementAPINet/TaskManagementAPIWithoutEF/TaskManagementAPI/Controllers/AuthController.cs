using Dapper;
using Npgsql;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers;

// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Dummy auth (replace with real check)
        /*
        if (request.Username != "admin" || request.Password != "password")
            return Unauthorized();
            
        */
        using var connection = new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
        const string sql = "SELECT id, username, passwordHash, role FROM users WHERE username = @Username";

        var user = await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new { request.Username });

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized();

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["JwtSettings:ExpireMinutes"]));

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}
/*
public class LoginRequest
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}
*/
