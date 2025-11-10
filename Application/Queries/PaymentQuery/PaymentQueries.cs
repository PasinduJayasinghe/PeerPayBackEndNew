using Application.Dtos;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.PaymentQuery
{
    public class GetPaymentByIdQuery : IRequest<PaymentDto?>
    {
        public string PaymentId { get; set; }
    }

    public class GetEmployerPaymentsQuery : IRequest<PaymentListDto>
    {
        public string EmployerId { get; set; }
    }

    public class GetStudentPaymentsQuery : IRequest<PaymentListDto>
    {
        public string StudentId { get; set; }
    }

    public class GetPaymentByJobQuery : IRequest<PaymentDto?>
    {
        public string JobId { get; set; }
    }

    public class GetPaymentsByStatusQuery : IRequest<IEnumerable<PaymentDto>>
    {
        public PaymentStatus Status { get; set; }
    }

    public class GetPaymentIntentStatusQuery : IRequest<object>
    {
        public string PaymentIntentId { get; set; }
    }
}
