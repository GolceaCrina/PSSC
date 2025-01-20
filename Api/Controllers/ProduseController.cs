using Microsoft.AspNetCore.Mvc;
using PSSC_Proiect.Data;
using PSSC_Proiect.Domain.Models;
using System.Text.Json;

namespace PSSC_Proiect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProduseController : ControllerBase
{
    private readonly ContextAplicatie _context;

    public ProduseController(ContextAplicatie context)
    {
        _context = context;
    }

    // GET: Preluare toate produsele
    [HttpGet]
    public IActionResult GetProduse()
    {
        var produse = _context.Produse.ToList();
        return Ok(produse);
    }

    // POST: Adăugare produs
    [HttpPost]
    public IActionResult AdaugaProdus([FromBody] Produs produs)
    {
        produs = produs with { Id = Guid.NewGuid() };
        _context.Produse.Add(produs);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetProduse), new { id = produs.Id }, produs);
    }

    // PUT: Modificare produs
    [HttpPatch("{id}")]
    public IActionResult ActualizeazaProdusPartial(Guid id, [FromBody] JsonElement campuriDeActualizat)
    {
        try
        {
            // Găsește produsul în baza de date
            var produs = _context.Produse.FirstOrDefault(p => p.Id == id);
            if (produs == null)
            {
                return NotFound($"Produsul cu ID-ul {id} nu a fost găsit.");
            }

            var produsEntry = _context.Entry(produs);

            // Iterează prin toate câmpurile primite pentru actualizare
            foreach (var prop in campuriDeActualizat.EnumerateObject())
            {
                if (prop.NameEquals("Nume") && prop.Value.ValueKind == JsonValueKind.String)
                {
                    produsEntry.Property(p => p.Nume).CurrentValue = prop.Value.GetString();
                }
                else if (prop.NameEquals("Pret") && prop.Value.ValueKind == JsonValueKind.Number)
                {
                    produsEntry.Property(p => p.Pret).CurrentValue = prop.Value.GetDecimal();
                }
                else if (prop.NameEquals("Stoc") && prop.Value.ValueKind == JsonValueKind.Number)
                {
                    produsEntry.Property(p => p.Stoc).CurrentValue = prop.Value.GetInt32();
                }
                else if (prop.NameEquals("Cod") && prop.Value.ValueKind == JsonValueKind.String)
                {
                    produsEntry.Property(p => p.Cod).CurrentValue = prop.Value.GetString();
                }
                else
                {
                    return BadRequest($"Câmpul {prop.Name} nu este recunoscut sau valoarea sa nu este validă.");
                }
            }

            // Salvează modificările în baza de date
            _context.SaveChanges();

            return Ok("Produsul a fost actualizat parțial cu succes.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Eroare la actualizarea produsului: {ex.Message}");
        }
    }


    // DELETE: Ștergere produs
    [HttpDelete("{id}")]
    public IActionResult StergeProdus(Guid id)
    {
        var produs = _context.Produse.Find(id);
        if (produs == null)
        {
            return NotFound($"Produsul cu ID {id} nu a fost găsit.");
        }

        _context.Produse.Remove(produs);
        _context.SaveChanges();

        return Ok("Produsul a fost șters.");
    }
}