using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using MemberNova.Admins;

[Table("Membresias")]
public class Membresia
{
    [Key]
    public int MiD { get; set; }

    //[MaxLength(60)]
    //public string Usuario { get; set; }

    [MaxLength(20)]
    public string Tipo { get; set; }

    [MaxLength(300)]
    public string Descripcion { get; set; }

    public decimal Total { get; set; }

    //Parametro por si el tipo de membresia es especial de algun tipo, con alguna disponibilidad exclusiva.
    public string IsExclusive { get; set; }

    public int NumMiembros { get; set; }
    public ICollection<Usuario> Usuarios { get; } = new List<Usuario>();

    public ICollection<Beneficio> Beneficios { get; } = new List<Beneficio>();

}
