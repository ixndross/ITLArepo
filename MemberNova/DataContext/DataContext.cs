
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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasOne(e => e.Membresia)
            .WithMany(e => e.Usuarios)
            .HasForeignKey(e => e.TipoMembresia)
            .IsRequired();

        modelBuilder.Entity<Beneficio>()
            .HasOne(e => e.Membresia)
            .WithMany(e => e.Beneficios)
            .HasForeignKey(e => e.TipoMembresia)
            .IsRequired();
        modelBuilder.Entity<Pago>()
            .HasOne(e => e.Usuario)
            .WithMany(e => e.Pagos)
            .HasForeignKey(e => e.UserChargedID)
            .IsRequired();
    }
}
