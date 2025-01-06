using Microsoft.AspNetCore.Mvc;
using PSSC_Proiect.Data;
using PSSC_Proiect.Domain.Models;

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

    [HttpGet]
    public IActionResult GetProduse()
    {
        var produse = _context.Produse.ToList();
        return Ok(produse);
    }

    [HttpPost]
    public IActionResult AdaugaProdus([FromBody] Produs produs)
    {
        produs = produs with { Id = Guid.NewGuid() };
        _context.Produse.Add(produs);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetProduse), new { id = produs.Id }, produs);
    }
}