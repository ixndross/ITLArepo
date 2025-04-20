
using MemberNova.Admins;
using Microsoft.EntityFrameworkCore;
using MemberNova.Helpers;

public class MemberNova_DataContext : DbContext
{
    public DbSet<UsuariosRegulares> UsuariosRegulares { get; set; }
    public DbSet<UsuariosVIP> UsuariosVIP { get; set; }
    public DbSet<Membresia> Membresias { get; set; }
    public DbSet<PagoRegular> PagosRegulares { get; set; }
    public DbSet<PagosVIP> PagosVIP { get; set; }
    public DbSet<Beneficio> Beneficios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=IxnDross\\SQLEXPRESS;Database=Membernova;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuariosRegulares>()
            .HasOne(e => e.Membresia)
            .WithMany(e => e.UsuariosRegulares)
            .HasForeignKey(e => e.TipoMembresia)
            .IsRequired();

        modelBuilder.Entity<UsuariosVIP>()
            .HasOne(e => e.Membresia)
            .WithMany(e => e.UsuariosVIP)
            .HasForeignKey(e => e.TipoMembresia)
            .IsRequired();

        modelBuilder.Entity<Beneficio>()
            .HasOne(e => e.Membresia)
            .WithMany(e => e.Beneficios)
            .HasForeignKey(e => e.TipoMembresia)
            .IsRequired();

        modelBuilder.Entity<PagoRegular>()
            .HasOne(e => e.Usuario)
            .WithMany(e => e.Pagos)
            .HasForeignKey(e => e.UserChargedID)
            .IsRequired();

        modelBuilder.Entity<PagosVIP>()
            .HasOne(e => e.Usuario)
            .WithMany(e => e.Pagos)
            .HasForeignKey(e => e.VIPUserChargedID)
            .IsRequired();
    }
}
