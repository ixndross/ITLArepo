using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

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

    public SqlMoney Total { get; set; }

    //Parametro por si el tipo de membresia es especial de algun tipo, con alguna disponibilidad exclusiva.
    public string IsExclusive { get; set; }

}
