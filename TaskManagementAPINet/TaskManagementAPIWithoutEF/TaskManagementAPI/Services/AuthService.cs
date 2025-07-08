// Services/AuthService.cs
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services;


public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _config = config;
    }

    public async Task<string?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepo.GetByUsernameAsync(username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
       new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
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

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<int> RegisterUserAsync(string username, string password, string role)
    {
        var existingUser = await _userRepo.GetByUsernameAsync(username);
        if (existingUser != null)
        {
            throw new Exception("User already exists.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        return await _userRepo.RegisterUserAsync(username, passwordHash, role);
    }
}