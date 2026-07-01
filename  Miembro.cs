namespace IglesiaZoarAPI;

public class Miembro
{
    public int Id { get; set; }
    public string CodigoMiembro { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string TipoMiembro { get; set; } = "Propiedad"; // Propiedad o Catecumeno
    public bool EsDiacono { get; set; }
    public bool EsDiaconisa { get; set; }
    public bool TieneCargo { get; set; }
    public string? DetalleCargo { get; set; }
}
