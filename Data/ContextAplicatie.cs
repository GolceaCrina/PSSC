using Microsoft.EntityFrameworkCore;
using PSSC_Proiect.Domain.Models;

namespace PSSC_Proiect.Data;

public class ContextAplicatie : DbContext
{
    public ContextAplicatie(DbContextOptions<ContextAplicatie> options) : base(options) { }

    public DbSet<Produs> Produse { get; set; }
    public DbSet<Comanda> Comenzi { get; set; }
    public DbSet<LinieComanda> LiniiComanda { get; set; }
    public DbSet<Factura> Facturi { get; set; }
    public DbSet<Expediere> Expedieri { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurarea entității Produs
        modelBuilder.Entity<Produs>(entity =>
        {
            entity.HasKey(p => p.Id); // Cheia primară
            entity.Property(p => p.Cod).IsRequired(); // Cod este obligatoriu
            entity.Property(p => p.Nume).IsRequired(); // Nume este obligatoriu
            entity.Property(p => p.Pret).HasColumnType("decimal(18,2)"); // Tip decimal
            entity.Property(p => p.Stoc).IsRequired(); // Stoc este obligatoriu
        });

        // Configurarea entității Comanda
        modelBuilder.Entity<Comanda>(entity =>
        {
            entity.HasKey(c => c.Id); // Cheia primară
            entity.Property(c => c.Adresa).IsRequired(); // Adresa este obligatorie
            entity.Property(c => c.Total).HasColumnType("decimal(18,2)"); // Tip decimal
        });

        // Configurarea entității LinieComanda
        modelBuilder.Entity<LinieComanda>(entity =>
        {
            entity.HasKey(l => l.Id); // Cheia primară
            entity.Property(l => l.Cantitate).IsRequired(); // Cantitatea este obligatorie
            entity.Property(l => l.Pret).HasColumnType("decimal(18,2)"); // Tip decimal
            entity.HasOne<Comanda>()
                .WithMany(c => c.LiniiComanda)
                .HasForeignKey(l => l.ComandaId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<Produs>()
                .WithMany()
                .HasForeignKey(l => l.ProdusId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configurarea entității Factura
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(f => f.Id); // Cheia primară
            entity.Property(f => f.Total).HasColumnType("decimal(18,2)"); // Tip decimal
        });

        // Configurarea entității Expediere
        modelBuilder.Entity<Expediere>(entity =>
        {
            entity.HasKey(e => e.Id); // Cheia primară
        });
    }
}
