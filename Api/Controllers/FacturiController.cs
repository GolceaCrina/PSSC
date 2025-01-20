using Microsoft.AspNetCore.Mvc;
using PSSC_Proiect.Data.Repositories;
using PSSC_Proiect.Domain.Models;

namespace PSSC_Proiect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacturiController : ControllerBase
{
    private readonly FacturaRepository _facturaRepository;

    public FacturiController(FacturaRepository facturaRepository)
    {
        _facturaRepository = facturaRepository;
    }

    // GET: Obține toate facturile
    [HttpGet]
    public IActionResult GetFacturi()
    {
        var facturi = _facturaRepository.GetToateFacturile();
        return Ok(facturi);
    }

    // GET: Obține o factură după ID
    [HttpGet("{id}")]
    public IActionResult GetFactura(Guid id)
    {
        var factura = _facturaRepository.GetFactura(id);
        if (factura == null)
        {
            return NotFound("Factura nu a fost găsită.");
        }

        return Ok(factura);
    }

    // POST: Generează o factură
    [HttpPost("{comandaId}")]
    public IActionResult GenereazaFactura(Guid comandaId, [FromBody] decimal total)
    {
        var factura = new Factura
        {
            Id = Guid.NewGuid(),
            ComandaId = comandaId,
            Total = total
        };

        _facturaRepository.AdaugaFactura(factura);

        return Ok(factura);
    }

    // DELETE: Șterge o factură după ID
    [HttpDelete("{id}")]
    public IActionResult StergeFactura(Guid id)
    {
        var factura = _facturaRepository.GetFactura(id);
        if (factura == null)
        {
            return NotFound("Factura nu a fost găsită.");
        }

        _facturaRepository.StergeFactura(factura);
        return Ok("Factura a fost ștearsă cu succes.");
    }
}