using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;

namespace IglesiaZoarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrivilegiosController : ControllerBase
{
    private readonly string _connectionString;
    public PrivilegiosController(IConfiguration config) => _connectionString = config.GetConnectionString("DefaultConnection")!;

    // Trae la lista de hermanos sugeridos para privilegios (los que llevan más tiempo sin participar)
    [HttpGet("sugerencias")]
    public async Task<IActionResult> ObtenerSugerencias()
    {
        using var conn = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT m.id as MiembroId, m.nombre_completo as NombreCompleto, to_char(MAX(p.fecha), 'YYYY-MM-DD') AS fecha
                    FROM miembros m LEFT JOIN privilegios p ON m.id = p.miembro_id WHERE m.activo = TRUE
                    GROUP BY m.id, m.nombre_completo ORDER BY MAX(p.fecha) ASC NULLS FIRST, m.nombre_completo ASC;";
        return Ok(await conn.QueryAsync<Privilegio>(sql));
    }

    [HttpPost]
    public async Task<IActionResult> Guardar([FromBody] Privilegio pr)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.ExecuteAsync("INSERT INTO privilegios (miembro_id, culto, fecha, descripcion_privilegio) VALUES (@MiembroId, @Culto::culto_enum, @Fecha::DATE, @DescripcionPrivilegio);", pr);
        return Ok(new { mensaje = "Privilegio asignado con éxito" });
    }
}
