using Infra.Library;
using Infra.Library.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Library;

namespace TutorsCollageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentservice;
        public PaymentController(PaymentService paymentservice)
        {
            _paymentservice = paymentservice;
        }

        [HttpGet("GetByAllPayment")]
        public async Task<ActionResult<List<Payment>>> DisplayAll()
        {
            return await _paymentservice.GetAll();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Payment>> DisplayOne(int id)
        {
            try
            {
                var payment = await _paymentservice.GetOne(id);
                return Ok(payment);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("AddPaymentRecord")]
        public async Task<ActionResult<Payment>> AddPayment([FromBody] Payment payment)
        {
            try
            {
                await _paymentservice.Add(payment);
                return Ok(new
                {
                    message = "✅ Data Successfully Added",
                    PaymentId = payment.PaymentId,
                    StudentId = payment.StudentId,
                    //StudentName = payment.Student.FirstName + " " + payment.Student.LastName,
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("UpdatePaymentRecords/{id}")]
        public async Task<ActionResult<Payment>> upadatePay(int id, Payment payment)
        {
            try
            {
                await _paymentservice.Update(id, payment);
                return Ok(new
                {
                    message = "✅ Data Successfully Updated",
                    UpdatedPaymentId = payment.PaymentId,
                    UpdatedStudentId = payment.StudentId,
                    
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("DeletePaymentRecords/{id}")]
        public async Task<ActionResult<Payment>> DeletePay(int id)
        {
            try
            {
                await _paymentservice.Delete(id);
                return Ok(new
                {
                    message = "✅ Data Successfully  Deleted",
                    PaymentId = id,
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
