using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemberNova.Helpers;

namespace MemberNova.Admins
{
    [Table("Usuarios")]
    public class Usuario
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

        //Metodo para exponer nombre completo de usuario.
        public string GetFullName()
        {
            Name = Name is null ? "" : Name;
            return $"{Name} {LastName}";
        }
    }
} 