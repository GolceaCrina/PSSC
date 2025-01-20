namespace PSSC_Proiect.Domain.Models;

public record Factura
{
    public Guid Id { get; set; }
    public Guid ComandaId { get; set; }
    public DateTime DataEmiterii { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
}