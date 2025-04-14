﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Pagos")]
public class Pago
{
    [Key]
    [Column("ID")]
    public int PayID { get; set; }

    public DateTime Fecha { get; set; }

    [MaxLength(300)]
    public string Concepto { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Descuento { get; set; }

    public decimal Total { get; set; }

    public decimal GetTotal()
    {
        return Subtotal - Descuento;
    }

}
