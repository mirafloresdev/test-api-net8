using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;

namespace ApiTestDanielHernandez.Models;

[Table("users")]
public class User
{
    [Key]
    public int Id { get; set; }
    public string nombres { get; set; }
    public string apellidos { get; set; }
    public string alias { get; set; }
    public DateTime fechanacimiento { get; set; }
    public string direccion { get; set; }
    public string password { get; set; }
    public string telefono { get; set; }
    public string email { get; set; }
    
    public DateTime fechacreacion { get; set; }
    
    [Computed]
    public DateTime? fechamodificacion { get; set; }
}