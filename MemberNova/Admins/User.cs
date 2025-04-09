using System.ComponentModel.DataAnnotations;

namespace MemberNova.Admins
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(13)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string BillingAddress { get; set; }
        public int Age { get; set; }

    }
}