namespace PSSC_Proiect.Domain.Models;

public record Comanda
{
    public Guid Id { get; set; }
    public string Adresa { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = "Created";
    public List<LinieComanda> LiniiComanda { get; set; } = new();

    public Comanda() { }
    
    public Comanda(Guid id, string adresa, decimal total, string status, List<LinieComanda> liniiComanda)
    {
        Id = id;
        Adresa = adresa;
        Total = total;
        Status = status;
        LiniiComanda = liniiComanda;
    }
}