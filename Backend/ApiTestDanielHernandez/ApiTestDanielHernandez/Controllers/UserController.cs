
using ApiTestDanielHernandez.Data;
using ApiTestDanielHernandez.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiTestDanielHernandez.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    
    /// <summary>
    /// Obtenemos todos los usuarios sin restriccion.
    /// </summary>
    /// <returns>Listado de usuarios</returns>
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetAllUsers();
        return Ok(users);
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        try
        {
            _userRepository.AddUser(user);
            return Ok(new { message = "User registered successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    [Authorize]  // Asegura que solo usuarios autenticados puedan acceder a este método
    public IActionResult GetUserById(int id)
    {
        var user = _userRepository.obtenerUsuarioXId(id);
        if (user != null)
        {
            return Ok(user);
        }
        return BadRequest(new { message = "Autorizacion requerida" });
    }
    
    [HttpDelete("{id}")]
    [Authorize] // Asegura que solo usuarios autenticados puedan acceder a este método
    public IActionResult DeleteUser(int id)
    {
        var userExists = _userRepository.obtenerUsuarioXId(id);
        if (userExists == null)
        {
            return NotFound(new { message = "Usuario no encontrado." });
        }

        var deleted = _userRepository.eliminarUsuario(id);
        if (deleted)
        {
            return Ok(new { message = "Usuario eliminado exitosamente." });
        }
        return BadRequest(new { message = "No se pudo eliminar el usuario." });
    }
    
    
    
    [HttpPut("{id}")]
    [Authorize] 
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest(new { message = "No coincide id de usuario." });
        }

        var existingUser = _userRepository.obtenerUsuarioXId(id);
        if (existingUser == null)
        {
            return NotFound(new { message = "Usuario no encontrado..." });
        }

        var updatedUser = _userRepository.actualizarUsuario(user);
        return Ok(updatedUser);
    }
    
}