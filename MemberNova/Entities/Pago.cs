using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemberNova.Admins;


namespace MemberNova.Admins
{
    public abstract class Pago
    {
        public abstract decimal GetTotal();
    }

    [Table("Pagos")]
    public class PagoRegular : Pago
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

        public int UserChargedID { get; set; }
        public UsuariosRegulares Usuario { get; set; }

        public override decimal GetTotal()
        {
            return Subtotal - Descuento;
        }

        public PagoRegular()
        {
        }

        public PagoRegular(int payID, DateTime fecha, string concepto, decimal subtotal, decimal descuento, decimal total, int userChargedID)
        {
            PayID = payID;
            Fecha = fecha;
            Concepto = concepto;
            Subtotal = subtotal;
            Descuento = descuento;
            Total = total;
            UserChargedID = userChargedID;
        }
    }

    [Table("Pagos VIP")]
    public class PagosVIP : Pago
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

        public int VIPUserChargedID { get; set; }
        public UsuariosVIP Usuario { get; set; }

        public override decimal GetTotal()
        {
            return Subtotal - Descuento;
        }

        public PagosVIP()
        {
        }

        public PagosVIP(int payID, DateTime fecha, string concepto, decimal subtotal, decimal descuento, decimal total, int userChargedID)
        {
            PayID = payID;
            Fecha = fecha;
            Concepto = concepto;
            Subtotal = subtotal;
            Descuento = descuento;
            Total = total;
            VIPUserChargedID = userChargedID;
        }
    }
}