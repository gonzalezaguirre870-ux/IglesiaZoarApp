namespace IglesiaZoarAPI;

public class Miembro
{
    public int Id { get; set; }
    public string CodigoMiembro { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string TipoMiembro { get; set; } = "Propiedad"; // Propiedad o Catecumeno
    public bool TieneCargo { get; set; }
    public string? DetalleCargo { get; set; }
    
    // Nuevas propiedades que coinciden con los Checkboxes de tu formulario
    public bool PerteneceFemenil { get; set; }
    public bool PerteneceMisioneritas { get; set; }
    public bool PerteneceVarones { get; set; }
    public bool PerteneceExploradores { get; set; }
    public bool PerteneceEmbajadores { get; set; }
}
