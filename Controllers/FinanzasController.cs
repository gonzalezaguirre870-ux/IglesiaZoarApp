using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;

namespace IglesiaZoarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinanzasController : ControllerBase
{
    private readonly string _connectionString;
    public FinanzasController(IConfiguration config) => _connectionString = config.GetConnectionString("DefaultConnection")!;

    [HttpGet]
    public async Task<IActionResult> Obtener()
    {
        using var conn = new NpgsqlConnection(_connectionString);
        return Ok(await conn.QueryAsync<Finanza>("SELECT id, tipo, categoria, sociedad, monto, to_char(fecha, 'YYYY-MM-DD') as fecha, descripcion FROM finanzas ORDER BY fecha DESC;"));
    }

    [HttpPost]
    public async Task<IActionResult> Guardar([FromBody] Finanza fn)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.ExecuteAsync("INSERT INTO finanzas (tipo, categoria, sociedad, monto, fecha, descripcion) VALUES (@Tipo::movimiento_enum, @Categoria, @Sociedad, @Monto, @Fecha::DATE, @Descripcion);", fn);
        return Ok(new { mensaje = "Transacción financiera registrada" });
    }
}
