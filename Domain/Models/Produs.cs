namespace PSSC_Proiect.Domain.Models;

public record Produs(
    Guid Id,         
    string Cod,
    string Nume,     
    decimal Pret,    
    int Stoc          
);