namespace PSSC_Proiect.Domain.ValueObjects;

public record CodProdus(string Valoare)
{
    public static CodProdus DinString(string valoare)
    {
        if (string.IsNullOrWhiteSpace(valoare))
        {
            throw new ArgumentException("Codul produsului nu poate fi gol.");
        }
        return new CodProdus(valoare);
    }
}