using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

public class Pago
{
    [Key]
    public int PayID { get; set; }
    public DateTime Fecha { get; set; }

    public string Concepto { get; set; }
    public SqlMoney Total { get; set; }

    public SqlMoney Descuento { get; set; }

}
