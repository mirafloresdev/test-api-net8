
using ApiTestDanielHernandez.Models;
using ApiTestDanielHernandez.Services;
using Microsoft.AspNetCore.Mvc;


namespace ApiTestDanielHernandez.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
   
    private readonly IAutenticacionService _authService;

    public AuthController(IAutenticacionService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        var user = _authService.Authenticate(loginDto.Alias, loginDto.Password);
        if (user != null)
        {
            var token = _authService.GenerateJwtToken(user.alias); // Genera token con el alias del usuario
        
            // Crear el objeto de respuesta con los datos del usuario y el token
            var response = new 
            {
                user = new
                {
                    id = user.Id,
                    nombres = user.nombres,
                    apellidos = user.apellidos,
                    session_active = true, 
                    fechanacimiento = user.fechanacimiento.ToString("yyyy-MM-dd"),
                    email = user.email,
                    telefono = user.telefono,
                    password = user.password, 
                    address = user.direccion
                },
                access_token = token,
                token_type = "bearer"
            };

            return Ok(response);
        }
        return Unauthorized(new { message = "Credenciales inválidas" });
    }

   
    
}