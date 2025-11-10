using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class JobCreatedEvent
    {
        public DateTime OccurredOn { get; set; }
        public string JobId { get; set; }
        public string EmployerId { get; set; }
        public string Title { get; set; }

        public JobCreatedEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class ApplicationSubmittedEvent
    {
        public DateTime OccurredOn { get; set; }
        public string ApplicationId { get; set; }
        public string JobId { get; set; }
        public string StudentId { get; set; }

        public ApplicationSubmittedEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class PaymentCompletedEvent
    {
        public DateTime OccurredOn { get; set; }
        public string PaymentId { get; set; }
        public string StudentId { get; set; }
        public decimal Amount { get; set; }

        public PaymentCompletedEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class UserRegisteredEvent
    {
        public DateTime OccurredOn { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }

        public UserRegisteredEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class ApplicationStatusChangedEvent
    {
        public DateTime OccurredOn { get; set; }
        public string ApplicationId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string UpdatedBy { get; set; }

        public ApplicationStatusChangedEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class JobClosedEvent
    {
        public DateTime OccurredOn { get; set; }
        public string JobId { get; set; }
        public string EmployerId { get; set; }
        public string Reason { get; set; }

        public JobClosedEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class MessageSentEvent
    {
        public DateTime OccurredOn { get; set; }
        public string MessageId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string ConversationId { get; set; }

        public MessageSentEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class WithdrawalRequestedEvent
    {
        public DateTime OccurredOn { get; set; }
        public string WithdrawalId { get; set; }
        public string StudentId { get; set; }
        public decimal Amount { get; set; }

        public WithdrawalRequestedEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class RatingSubmittedEvent
    {
        public DateTime OccurredOn { get; set; }
        public string RatingId { get; set; }
        public string RaterId { get; set; }
        public string RatedUserId { get; set; }
        public int RatingValue { get; set; }
        public string JobId { get; set; }

        public RatingSubmittedEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class UserVerifiedEvent
    {
        public DateTime OccurredOn { get; set; }
        public string UserId { get; set; }
        public string VerificationType { get; set; }
        public string VerifiedBy { get; set; }

        public UserVerifiedEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }
}