using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

public class Pago
{
    [Key]
    public int PayiD { get; set; }
    public DateTime Fecha { get; set; }
    public SqlMoney Total { get; set; }

}
