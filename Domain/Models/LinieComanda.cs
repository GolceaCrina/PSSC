namespace PSSC_Proiect.Domain.Models;

public record LinieComanda
{
    public Guid Id { get; set; }
    public Guid ComandaId { get; set; }
    public Guid ProdusId { get; set; }
    public int Cantitate { get; set; }
    public decimal Pret { get; set; }

    public LinieComanda() { }

    public LinieComanda(Guid id, Guid comandaId, Guid produsId, int cantitate, decimal pret)
    {
        Id = id;
        ComandaId = comandaId;
        ProdusId = produsId;
        Cantitate = cantitate;
        Pret = pret;
    }
}