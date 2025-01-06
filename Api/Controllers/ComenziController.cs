using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult GetComenzi()
    {
        var comenzi = _context.Comenzi.ToList();
        return Ok(comenzi);
    }

    [HttpPost]
    public IActionResult AdaugaComanda([FromBody] Comanda comanda)
    {
        comanda = comanda with { Id = Guid.NewGuid() };
        _context.Comenzi.Add(comanda);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetComenzi), new { id = comanda.Id }, comanda);
    }
}