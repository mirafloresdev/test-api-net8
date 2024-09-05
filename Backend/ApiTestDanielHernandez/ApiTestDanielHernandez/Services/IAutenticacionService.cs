using ApiTestDanielHernandez.Models;

namespace ApiTestDanielHernandez.Services;

public interface IAutenticacionService
{
     User Authenticate(string alias, string password);
     string GenerateJwtToken(string username);
}