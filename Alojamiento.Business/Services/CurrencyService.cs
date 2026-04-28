using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alojamiento.Business.Interfaces;
using Alojamiento.DataManagement.Context;

namespace Alojamiento.Business.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly AlojamientoDbContext _context;

        public CurrencyService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> ConvertirMonedaAsync(decimal monto, string monedaOrigenCodigo, string monedaDestinoCodigo)
        {
            if (monedaOrigenCodigo.ToUpper() == monedaDestinoCodigo.ToUpper())
                return monto;

            var origen = await _context.Monedas.FirstOrDefaultAsync(m => m.Codigo == monedaOrigenCodigo.ToUpper());
            var destino = await _context.Monedas.FirstOrDefaultAsync(m => m.Codigo == monedaDestinoCodigo.ToUpper());

            if (origen == null || destino == null)
                return monto; // Fallback: retornar original si no hay info

            var tasaCambio = await _context.TasasCambios
                .FirstOrDefaultAsync(t => t.MonedaOrigenId == origen.MonedaId && t.MonedaDestinoId == destino.MonedaId);

            if (tasaCambio != null)
            {
                return monto * tasaCambio.Tasa;
            }
            
            // Reintentar de forma inversa y dividir
            var tasaInversa = await _context.TasasCambios
                .FirstOrDefaultAsync(t => t.MonedaOrigenId == destino.MonedaId && t.MonedaDestinoId == origen.MonedaId);

            if (tasaInversa != null && tasaInversa.Tasa > 0)
            {
                return monto / tasaInversa.Tasa;
            }

            return monto; // Fallback final
        }
    }
}
