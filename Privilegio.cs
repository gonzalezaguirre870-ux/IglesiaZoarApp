namespace IglesiaZoarAPI;

public class Privilegio
{
    public int Id { get; set; }
    public int MiembroId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Culto { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public string DescripcionPrivilegio { get; set; } = string.Empty;
}
