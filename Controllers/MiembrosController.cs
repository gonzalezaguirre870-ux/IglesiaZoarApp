using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;

namespace IglesiaZoarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MiembrosController : ControllerBase
{
    private readonly string _connectionString;

    public MiembrosController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    // 1. OBTENER MIEMBROS ORDENADOS DE LA A-Z
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT id, codigo_miembro, nombre_completo, telefono, tipo_miembro, es_diacono, es_diaconisa, tiene_cargo, detalle_cargo FROM miembros ORDER BY nombre_completo ASC;";
        var miembros = await connection.QueryAsync<Miembro>(sql);
        return Ok(miembros);
    }

    // 2. GUARDAR UN NUEVO MIEMBRO
    [HttpPost]
    public async Task<IActionResult> Guardar([FromBody] Miembro miembro)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"INSERT INTO miembros (nombre_completo, telefono, tipo_miembro, es_diacono, es_diaconisa, tiene_cargo, detalle_cargo) 
                    VALUES (@NombreCompleto, @Telefono, @TipoMiembro::tipo_miembro_enum, @EsDiacono, @EsDiaconisa, @TieneCargo, @DetalleCargo);";
        
        await connection.ExecuteAsync(sql, miembro);
        return Ok(new { mensaje = "Miembro registrado con éxito en Iglesia Zoar" });
    }
}
