namespace PSSC_Proiect.Domain.ValueObjects;
public record AdresaLivrare(string Valoare)
{
    public static AdresaLivrare DinString(string valoare)
    {
        if (string.IsNullOrWhiteSpace(valoare))
        {
            throw new ArgumentException("Adresa de livrare nu poate fi goală.");
        }
        return new AdresaLivrare(valoare);
    }
}
