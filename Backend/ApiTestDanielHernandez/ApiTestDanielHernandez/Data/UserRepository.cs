using Dapper.Contrib.Extensions;
using Npgsql;

namespace ApiTestDanielHernandez.Data;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ApiTestDanielHernandez.Models;

public class UserRepository: IUserRepository
{
    
    private readonly string _connectionString;
    
    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    // metodo para obtener todos los usuarios sin filtro
    public IEnumerable<User> GetAllUsers()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            return connection.Query<User>("SELECT * FROM Users");
        }
    }

    
    // agregar un usuario
    public void AddUser(User user)
    {
        user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
        user.fechanacimiento = DateTime.Parse(user.fechanacimiento.ToString());
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Insert<User>(user);
        }
    }
    
    // obtener usuario por alias
    public User obtenerUsuarioXAlias(string alias)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            return connection.Query<User>($"SELECT * FROM Users WHERE alias = '{alias}'").FirstOrDefault();
        }
    }

    // obtener usuario por id
    public User obtenerUsuarioXId(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            return connection.QuerySingleOrDefault<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        }
    }

    public bool eliminarUsuario(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var result = connection.Execute("DELETE FROM Users WHERE Id = @Id", new { Id = id });
            return result > 0; // Retorna true si se eliminó algún registro
        }
    }

    public User actualizarUsuario(User user)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var sql = @"
                    UPDATE Users SET
                    alias = @alias,
                    password = @password,
                    nombres = @nombres,
                    apellidos = @apellidos,
                    direccion = @direccion,
                    email = @email,
                    fechanacimiento = @fechanacimiento
           
                    WHERE Id = @id";

            connection.Execute(sql, user);
            return obtenerUsuarioXId(user.Id); // Retornamos informacion actualizada
        }
    }
}