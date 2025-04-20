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
    //public string UsuarioRegulares { get; set; }

    [MaxLength(20)]
    public string Tipo { get; set; }

    [MaxLength(300)]
    public string Descripcion { get; set; }

    public decimal Total { get; set; }

    //Parametro por si el tipo de membresia es especial de algun tipo, con alguna disponibilidad exclusiva.
    public string IsExclusive { get; set; }

    public int NumMiembros { get; set; }
    public ICollection<UsuariosRegulares> UsuariosRegulares { get; } = new List<UsuariosRegulares>();
    public ICollection<UsuariosVIP> UsuariosVIP { get; } = new List<UsuariosVIP>();

    public ICollection<Beneficio> Beneficios { get; } = new List<Beneficio>();

    public Membresia()
    {
        /////////////////
    }
    public Membresia(string tipo, string descripcion, decimal total, string isExclusive, int numMiembros)
    {
        Tipo = tipo;
        Descripcion = descripcion;
        Total = total;
        IsExclusive = isExclusive;
        NumMiembros = numMiembros;
        //Constructor manual.
    }

}