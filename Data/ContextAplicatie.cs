using Microsoft.EntityFrameworkCore;
using PSSC_Proiect.Domain.Models;

namespace PSSC_Proiect.Data;

public class ContextAplicatie : DbContext
{
    public ContextAplicatie(DbContextOptions<ContextAplicatie> options) : base(options) { }

    public DbSet<Produs> Produse { get; set; }
    public DbSet<Comanda> Comenzi { get; set; }
    public DbSet<LinieComanda> LiniiComanda { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LinieComanda>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.HasOne<Comanda>()
                .WithMany()
                .HasForeignKey(l => l.ComandaId);
            entity.HasOne<Produs>()
                .WithMany()
                .HasForeignKey(l => l.ProdusId);
        });
    }
}