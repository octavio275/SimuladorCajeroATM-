using ChallengeNET.DataAccess.Entitys;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNET.DataAccess
{
  public class OperacionContext : DbContext
  {
    public OperacionContext(DbContextOptions<OperacionContext> options) : base(options)
    {

    }

    public DbSet<Tarjeta> Tarjetas { get; set; }
    public DbSet<Operacion> Operaciones { get; set; }
    public DbSet<Retiro> Retiros { get; set; }
    public DbSet<Balance> Balances { get; set; }

    /*   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación entre Retiro y Tarjeta
        modelBuilder.Entity<Retiro>()
            .HasOne(r => r.Tarjeta)
            .WithMany()
            .HasForeignKey(r => r.TarjetaId);

        // Configuración de la restricción de clave externa en Tarjeta para evitar actualización en cascada
        modelBuilder.Entity<Tarjeta>()
            .HasMany(t => t.Retiros)
            .WithOne(r => r.Tarjeta)
            .OnDelete(DeleteBehavior.Restrict);
    }*/
  }


}
