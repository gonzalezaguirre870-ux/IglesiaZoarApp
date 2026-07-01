using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;

namespace IglesiaZoarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SantaCenaController : ControllerBase
{
    private readonly string _connectionString;

    public SantaCenaController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    [HttpPost]
    public async Task<IActionResult> Guardar([FromBody] RegistroSantaCena registro)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"INSERT INTO santa_cena (miembro_id, fecha_domingo, asistio) 
                    VALUES (@MiembroId, @FechaDomingo::DATE, @Asistio);";
        
        await connection.ExecuteAsync(sql, registro);
        return Ok(new { mensaje = "Asistencia de Santa Cena guardada" });
    }
}

public class RegistroSantaCena
{
    public int MiembroId { get; set; }
    public string FechaDomingo { get; set; } = string.Empty;
    public bool Asistio { get; set; }
}
