namespace PSSC_Proiect.Domain.ValueObjects;

public record Pret(decimal Valoare)
{
    public static Pret DinDecimal(decimal valoare)
    {
        if (valoare <= 0)
        {
            throw new ArgumentException("Prețul trebuie să fie pozitiv.");
        }
        return new Pret(valoare);
    }
}