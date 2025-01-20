using PSSC_Proiect.Domain.Models;

namespace PSSC_Proiect.Data.Repositories;

public class ExpediereRepository
{
    private readonly ContextAplicatie _context;

    public ExpediereRepository(ContextAplicatie context)
    {
        _context = context;
    }

    public void AdaugaExpediere(Expediere expediere)
    {
        _context.Expedieri.Add(expediere);
        _context.SaveChanges();
    }

    public Expediere? GetExpediere(Guid id)
    {
        return _context.Expedieri.FirstOrDefault(e => e.Id == id);
    }

    // Obține toate expedierile
    public List<Expediere> GetToateExpedierile()
    {
        return _context.Expedieri.ToList();
    }

    // Șterge o expediere
    public void StergeExpediere(Expediere expediere)
    {
        _context.Expedieri.Remove(expediere);
        _context.SaveChanges();
    }
}