using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentStore _store;

        public PaymentController(PaymentStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IActionResult GetPayments()
        {
            return Ok(_store.GetAll());
        }

        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] Payment payment)
        {
            var created = _store.Add(payment);
            return StatusCode(201, created);
        }

        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var payment = _store.GetById(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }
    }

    [ApiController]
    [Route("payment")]
    public class PaymentStatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "Payment Service Running" });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}