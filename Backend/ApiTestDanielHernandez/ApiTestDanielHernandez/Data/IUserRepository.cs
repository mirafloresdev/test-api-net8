using ApiTestDanielHernandez.Models;

namespace ApiTestDanielHernandez.Data;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    void AddUser(User user);
    
    User obtenerUsuarioXAlias(string alias);
    
    User obtenerUsuarioXId(int id);
    
    bool eliminarUsuario(int id);
    
    User actualizarUsuario(User user);
    
}