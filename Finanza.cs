namespace IglesiaZoarAPI;

public class Finanza
{
    public int Id { get; set; }
    public string Tipo { get; set; } = "Ingreso"; // Ingreso o Egreso
    public string Categoria { get; set; } = string.Empty;
    public string Sociedad { get; set; } = "General";
    public decimal Monto { get; set; }
    public string Fecha { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}
