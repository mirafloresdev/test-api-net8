using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiTestDanielHernandez.Data;
using ApiTestDanielHernandez.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiTestDanielHernandez.Services;

public class AutenticacionService: IAutenticacionService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AutenticacionService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public User Authenticate(string alias, string password)
    {
        var user = _userRepository.obtenerUsuarioXAlias(alias);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.password))
        {
            return user;
        }
        return null;
    }
    
    
    // Generar jwt
    public string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}