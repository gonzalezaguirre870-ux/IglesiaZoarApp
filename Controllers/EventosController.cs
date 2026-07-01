using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;

namespace IglesiaZoarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly string _connectionString;
    public EventosController(IConfiguration config) => _connectionString = config.GetConnectionString("DefaultConnection")!;

    [HttpGet]
    public async Task<IActionResult> Obtener()
    {
        using var conn = new NpgsqlConnection(_connectionString);
        return Ok(await conn.QueryAsync<Evento>("SELECT id, culto, to_char(fecha, 'YYYY-MM-DD') as fecha, descripcion FROM eventos ORDER BY fecha ASC;"));
    }

    [HttpPost]
    public async Task<IActionResult> Guardar([FromBody] Evento ev)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.ExecuteAsync("INSERT INTO eventos (culto, fecha, descripcion) VALUES (@Culto::culto_enum, @Fecha::DATE, @Descripcion);", ev);
        return Ok(new { mensaje = "Evento semanal guardado" });
    }
}
