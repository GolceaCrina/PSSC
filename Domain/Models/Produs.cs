namespace PSSC_Proiect.Domain.Models;

public record Produs
{
    public Guid Id { get; set; }
    public string Cod { get; set; }
    public string Nume { get; set; }
    public decimal Pret { get; set; }
    public int Stoc { get; set; } // Modificabil

    public Produs() { }

    public Produs(Guid id, string cod, string nume, decimal pret, int stoc)
    {
        Id = id;
        Cod = cod;
        Nume = nume;
        Pret = pret;
        Stoc = stoc;
    }
}