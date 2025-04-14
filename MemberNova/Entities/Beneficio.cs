using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Beneficios")]
public class Beneficio
{
    [Key]
    [Column("ID")]
    public int BiD { get; set; }

    [MaxLength(60)]
    public string Nombre { get; set; }

    [MaxLength(60)]
    public string TipoMembresia { get; set; }

    [MaxLength(500)]
    public string Descripcion { get; set; }
}