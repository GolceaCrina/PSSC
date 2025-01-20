using Microsoft.AspNetCore.Mvc;
using PSSC_Proiect.Data.Repositories;
using PSSC_Proiect.Domain.Models;

namespace PSSC_Proiect.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpedieriController : ControllerBase
    {
        private readonly ExpediereRepository _expediereRepository;

        public ExpedieriController(ExpediereRepository expediereRepository)
        {
            _expediereRepository = expediereRepository;
        }

        // Creează o expediere
        [HttpPost("{comandaId}")]
        public IActionResult CreeazaExpediere(Guid comandaId)
        {
            var expediere = new Expediere
            {
                Id = Guid.NewGuid(),
                ComandaId = comandaId
            };

            _expediereRepository.AdaugaExpediere(expediere);

            return Ok(expediere);
        }

        // Obține toate expedierile
        [HttpGet]
        public IActionResult GetToateExpedierile()
        {
            var expedieri = _expediereRepository.GetToateExpedierile();
            return Ok(expedieri);
        }

        // Obține o expediere după ID
        [HttpGet("{id}")]
        public IActionResult GetExpediere(Guid id)
        {
            var expediere = _expediereRepository.GetExpediere(id);
            if (expediere == null)
                return NotFound("Expedierea nu a fost găsită.");

            return Ok(expediere);
        }

        // Șterge o expediere
        [HttpDelete("{id}")]
        public IActionResult StergeExpediere(Guid id)
        {
            var expediere = _expediereRepository.GetExpediere(id);
            if (expediere == null)
                return NotFound("Expedierea nu a fost găsită.");

            _expediereRepository.StergeExpediere(expediere);
            return Ok("Expedierea a fost ștearsă cu succes.");
        }
    }
}