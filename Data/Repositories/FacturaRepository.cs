using PSSC_Proiect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSSC_Proiect.Data.Repositories
{
    public class FacturaRepository
    {
        private readonly ContextAplicatie _context;

        public FacturaRepository(ContextAplicatie context)
        {
            _context = context;
        }

        // Adaugă o factură în baza de date
        public void AdaugaFactura(Factura factura)
        {
            _context.Facturi.Add(factura);
            _context.SaveChanges();
        }

        // Obține o factură după ID
        public Factura? GetFactura(Guid id)
        {
            return _context.Facturi.FirstOrDefault(f => f.Id == id);
        }

        // Obține toate facturile
        public List<Factura> GetToateFacturile()
        {
            return _context.Facturi.ToList();
        }

        // Șterge o factură
        public void StergeFactura(Factura factura)
        {
            _context.Facturi.Remove(factura);
            _context.SaveChanges();
        }
    }
}