namespace PSSC_Proiect.Domain.Models;

public record LinieComanda(
    Guid Id,
    Guid ComandaId,
    Guid ProdusId,
    int Cantitate,
    decimal Pret
);