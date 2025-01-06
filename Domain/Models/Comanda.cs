namespace PSSC_Proiect.Domain.Models;

public record Comanda(
    Guid Id,
    string Adresa,
    decimal Total
);