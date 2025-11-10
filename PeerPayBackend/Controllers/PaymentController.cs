using Application.Commands.PaymentCommand;
using Application.Dtos;
using Application.Queries.PaymentQuery;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/payment/create
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
        {
            var command = new CreatePaymentCommand
            {
                JobId = dto.JobId,
                EmployerId = dto.EmployerId,
                StudentId = dto.StudentId,
                Amount = dto.Amount,
                Currency = dto.Currency,
                Notes = dto.Notes
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST: api/payment/confirm
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentDto dto)
        {
            var command = new ConfirmPaymentCommand
            {
                PaymentId = dto.PaymentId,
                PaymentIntentId = dto.PaymentIntentId
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST: api/payment/refund
        [HttpPost("refund")]
        public async Task<IActionResult> RefundPayment([FromBody] RefundPaymentDto dto)
        {
            var command = new RefundPaymentCommand
            {
                PaymentId = dto.PaymentId,
                RefundAmount = dto.RefundAmount,
                Reason = dto.Reason
            };

            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Payment refunded successfully" });
        }

        // POST: api/payment/{id}/cancel
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelPayment(string id)
        {
            var command = new CancelPaymentCommand { PaymentId = id };
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Payment cancelled successfully" });
        }

        // GET: api/payment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(string id)
        {
            var query = new GetPaymentByIdQuery { PaymentId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Payment not found" });
            }

            return Ok(result);
        }

        // GET: api/payment/job/{jobId}
        [HttpGet("job/{jobId}")]
        public async Task<IActionResult> GetPaymentByJob(string jobId)
        {
            var query = new GetPaymentByJobQuery { JobId = jobId };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Payment not found for this job" });
            }

            return Ok(result);
        }

        // GET: api/payment/employer/{employerId}
        [HttpGet("employer/{employerId}")]
        public async Task<IActionResult> GetEmployerPayments(string employerId)
        {
            var query = new GetEmployerPaymentsQuery { EmployerId = employerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/payment/student/{studentId}
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentPayments(string studentId)
        {
            var query = new GetStudentPaymentsQuery { StudentId = studentId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/payment/status/{status}
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetPaymentsByStatus(PaymentStatus status)
        {
            var query = new GetPaymentsByStatusQuery { Status = status };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/payment/intent/{paymentIntentId}/status
        [HttpGet("intent/{paymentIntentId}/status")]
        public async Task<IActionResult> GetPaymentIntentStatus(string paymentIntentId)
        {
            var query = new GetPaymentIntentStatusQuery { PaymentIntentId = paymentIntentId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
