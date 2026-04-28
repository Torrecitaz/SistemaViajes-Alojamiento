using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Alojamiento.Business.Interfaces;

namespace Alojamiento.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public class PaymentRequestDTO
        {
            public int ReservaId { get; set; }
            public decimal Monto { get; set; }
            public string Moneda { get; set; } = "USD";
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDTO request)
        {
            try
            {
                bool result = await _paymentService.ProcessPaymentAsync(request.ReservaId, request.Monto, request.Moneda);
                return Ok(new { success = result, message = "Pago procesado y factura generada exitosamente." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
