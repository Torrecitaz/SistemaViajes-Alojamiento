namespace Alojamiento.Business.Interfaces
{
    public interface ICurrencyService
    {
        Task<decimal> ConvertirMonedaAsync(decimal monto, string monedaOrigenCodigo, string monedaDestinoCodigo);
    }
}
