using PSSC_Proiect.Data;
using PSSC_Proiect.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace PSSC_Proiect.Data.Repositories;

public interface IComandaRepository
{
    Task<List<Comanda>> GetComenziAsync();
    Task<Comanda?> GetComandaByIdAsync(Guid id);
    Task AdaugaComandaAsync(Comanda comanda);
}

public class ComandaRepository : IComandaRepository
{
    private readonly ContextAplicatie _context;

    public ComandaRepository(ContextAplicatie context)
    {
        _context = context;
    }

    public async Task<List<Comanda>> GetComenziAsync()
    {
        return await _context.Comenzi.ToListAsync();
    }

    public async Task<Comanda?> GetComandaByIdAsync(Guid id)
    {
        return await _context.Comenzi.FindAsync(id);
    }

    public async Task AdaugaComandaAsync(Comanda comanda)
    {
        await _context.Comenzi.AddAsync(comanda);
        await _context.SaveChangesAsync();
    }
}