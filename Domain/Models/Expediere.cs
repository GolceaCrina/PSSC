namespace PSSC_Proiect.Domain.Models;

public record Expediere
{
    public Guid Id { get; set; }
    public Guid ComandaId { get; set; }
    public string Status { get; set; } = "Pregătită";
    public DateTime DataExpedierii { get; set; } = DateTime.UtcNow;
}