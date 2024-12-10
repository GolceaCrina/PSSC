namespace PSSC_Proiect.Domain.Models;

public interface IQuantity
{
    decimal GetValue(); // Returnează valoarea numerică a cantității
}

public record UnitQuantity(int Units) : IQuantity
{
    public decimal GetValue() => Units;
}

public record KilogramQuantity(decimal Kilograms) : IQuantity
{
    public decimal GetValue() => Kilograms;
}
