using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemberNova.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace MemberNova.Admins
{
    
    interface IUsuariosBase
    {
        public string GetFullName();

    }
    [Table("Usuarios")]
    public class UsuariosRegulares : IUsuariosBase
    {

        [Key]
        public int ID { get; set; }

        [Column("Nombre")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column("Apellido")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Column("Telefono")]
        [MaxLength(13)]
        public string Phone { get; set; }

        [Column("Correo")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Column("Direccion")]
        [MaxLength(200)]
        public string BillingAddress { get; set; }

        [Column("Edad")]
        public int Age { get; set; }

        //Llave foranea dentro de la base de datos, relacionando al usuario con una membresia con ID unico.
        [ForeignKey("TipoMembresia")]
        public int TipoMembresia { get; set; }
        //Referencia de navegacion a la entidad Membresia.
        public Membresia Membresia { get; set; } = null;

        public ICollection<PagoRegular> Pagos { get; } = new List<PagoRegular>();

        //Metodo para exponer nombre completo de usuario.
        public string GetFullName()
        {
            if (Name.IsNullOrEmpty())
            {
                return $"{LastName}";
            }
            else
            {
                return $"{Name} {LastName}";
            }
        }

        public UsuariosRegulares()
        {
            //Constructor por defecto
        }
        public UsuariosRegulares(string name, string lastname, string phone, string email, string address, int age, int tipoMembresia)
        {
            Name = name;
            LastName = lastname;
            Phone = phone;
            Email = email;
            BillingAddress = address;
            Age = age;
            TipoMembresia = tipoMembresia;
        }
    }


    //Clase para usuarios VIP, que hereda de la interfaz creada.
    [Table("Usuarios VIP")]
    public class UsuariosVIP : IUsuariosBase
    {
        [Key]
        public int ID { get; set; }

        [Column("Nombre")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column("Apellido")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Column("Telefono")]
        [MaxLength(13)]
        public string Phone { get; set; }

        [Column("Correo")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Column("Direccion")]
        [MaxLength(200)]
        public string BillingAddress { get; set; }

        [Column("Edad")]
        public int Age { get; set; }

        //Llave foranea dentro de la base de datos, relacionando al usuario con una membresia con ID unico.
        [ForeignKey("TipoMembresia")]
        public int TipoMembresia { get; set; }
        //Referencia de navegacion a la entidad Membresia.
        public Membresia Membresia { get; set; } = null;

        public ICollection<PagosVIP> Pagos { get; } = new List<PagosVIP>();

        //Metodo para exponer nombre completo de usuario.
        public string GetFullName()
        {
            if (Name.IsNullOrEmpty())
            {
                return $"{LastName}";
            }
            else
            {
                return $"{Name} {LastName}";
            }
        }

        public UsuariosVIP()
        {
            //Constructor por defecto
        }
        public UsuariosVIP(string name, string lastname, string phone, string email, string address, int age, int tipoMembresia)
        {
            Name = name;
            LastName = lastname;
            Phone = phone;
            Email = email;
            BillingAddress = address;
            Age = age;
            TipoMembresia = tipoMembresia;
        }

    }
} 