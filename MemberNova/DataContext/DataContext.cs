
using MemberNova.Admins;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Membresia> Membresias { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Beneficio> Beneficios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=IxnDross\\SQLEXPRESS;Database=Membernova;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
    }
}
