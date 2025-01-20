using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSSC_Proiect.Data;
using PSSC_Proiect.Domain.Models;

namespace PSSC_Proiect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComenziController : ControllerBase
{
    private readonly ContextAplicatie _context;

    public ComenziController(ContextAplicatie context)
    {
        _context = context;
    }

    // Metodă pentru a actualiza stocul unui produs
    private void ActualizeazaStocProdus(Guid produsId, int cantitate, bool adauga)
    {
        var produs = _context.Produse.Find(produsId);
        if (produs == null)
        {
            throw new Exception($"Produsul cu ID {produsId} nu există.");
        }

        produs.Stoc = adauga ? produs.Stoc + cantitate : produs.Stoc - cantitate;

        if (produs.Stoc < 0)
        {
            throw new Exception($"Stoc insuficient pentru produsul cu ID {produsId}.");
        }

        _context.Entry(produs).State = EntityState.Modified;
    }

    // Preluare toate comenzile
    [HttpGet]
    public IActionResult GetComenzi()
    {
        try
        {
            var comenzi = _context.Comenzi.Include(c => c.LiniiComanda).ToList();
            return Ok(comenzi);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Eroare la preluarea comenzilor: {ex.Message}");
        }
    }

    // Preluare comandă după ID
    [HttpGet("{id}")]
    public IActionResult GetComanda(Guid id)
    {
        try
        {
            var comanda = _context.Comenzi
                .Include(c => c.LiniiComanda)
                .FirstOrDefault(c => c.Id == id);

            if (comanda == null)
            {
                return NotFound("Comanda nu a fost găsită.");
            }

            return Ok(comanda);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Eroare la preluarea comenzii: {ex.Message}");
        }
    }

    // Adăugare comandă
    [HttpPost]
    public IActionResult AdaugaComanda([FromBody] Comanda comanda)
    {
        try
        {
            var liniiComandaActualizate = new List<LinieComanda>();

            foreach (var linie in comanda.LiniiComanda)
            {
                ActualizeazaStocProdus(linie.ProdusId, linie.Cantitate, false);

                var produs = _context.Produse.Find(linie.ProdusId);

                liniiComandaActualizate.Add(new LinieComanda
                {
                    ProdusId = linie.ProdusId,
                    Cantitate = linie.Cantitate,
                    Pret = produs.Pret
                });
            }

            comanda.LiniiComanda = liniiComandaActualizate;
            comanda.Total = liniiComandaActualizate.Sum(l => l.Cantitate * l.Pret);
            comanda.Status = "Created";

            _context.Comenzi.Add(comanda);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetComanda), new { id = comanda.Id }, comanda);
        }
        catch (Exception ex)
        {
            return BadRequest($"Eroare la adăugarea comenzii: {ex.Message}");
        }
    }

    // Anulare comandă
    [HttpPost("{id}/cancel")]
    public IActionResult AnuleazaComanda(Guid id)
    {
        try
        {
            var comanda = _context.Comenzi
                .Include(c => c.LiniiComanda)
                .FirstOrDefault(c => c.Id == id);

            if (comanda == null)
            {
                return NotFound("Comanda nu a fost găsită.");
            }

            if (comanda.Status != "Created")
            {
                return BadRequest("Comanda nu poate fi anulată.");
            }

            foreach (var linie in comanda.LiniiComanda)
            {
                ActualizeazaStocProdus(linie.ProdusId, linie.Cantitate, true);
            }

            comanda.Status = "Cancelled";
            _context.Entry(comanda).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok("Comanda a fost anulată.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Eroare la anularea comenzii: {ex.Message}");
        }
    }

    // Modificare comandă
    [HttpPut("{id}")]
    public IActionResult ModificaComanda(Guid id, [FromBody] Comanda comandaActualizata)
    {
        try
        {
            var comanda = _context.Comenzi
                .Include(c => c.LiniiComanda)
                .FirstOrDefault(c => c.Id == id);

            if (comanda == null)
            {
                return NotFound("Comanda nu a fost găsită.");
            }

            if (comanda.Status != "Created")
            {
                return BadRequest("Comanda nu poate fi modificată.");
            }

            foreach (var linie in comanda.LiniiComanda)
            {
                ActualizeazaStocProdus(linie.ProdusId, linie.Cantitate, true);
            }

            var liniiComandaNoi = new List<LinieComanda>();
            foreach (var linieNoua in comandaActualizata.LiniiComanda)
            {
                ActualizeazaStocProdus(linieNoua.ProdusId, linieNoua.Cantitate, false);

                var produs = _context.Produse.Find(linieNoua.ProdusId);

                liniiComandaNoi.Add(new LinieComanda
                {
                    ProdusId = linieNoua.ProdusId,
                    Cantitate = linieNoua.Cantitate,
                    Pret = produs.Pret
                });
            }

            comanda.LiniiComanda = liniiComandaNoi;
            comanda.Total = liniiComandaNoi.Sum(l => l.Cantitate * l.Pret);

            _context.Entry(comanda).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok("Comanda a fost actualizată.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Eroare la modificarea comenzii: {ex.Message}");
        }
    }
}
