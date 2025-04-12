using System.ComponentModel.DataAnnotations;

public class Beneficios
{
    [Key]
    public int BiD { get; set; }

    [MaxLength(30)]
    public string Nombre { get; set; }

    [MaxLength(60)]
    public string TipoMembresia { get; set; }

    [MaxLength(500)]
    public string Descripcion { get; set; }
}