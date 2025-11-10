using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum UserType
    {
        Student = 1,
        Employer = 2,
        Admin = 3
    }

    public enum UserStatus
    {
        Active = 1,
        Inactive = 2,
        Suspended = 3,
        PendingVerification = 4
    }

    public enum JobStatus
    {
        Active = 1,
        Closed = 2,
        Completed = 3,
        Cancelled = 4
    }

    public enum JobType
    {
        FullTime = 1,
        PartTime = 2,
        ProjectBased = 3,
        Freelance = 4
    }

    public enum ApplicationStatus
    {
        Submitted = 1,
        UnderReview = 2,
        Shortlisted = 3,
        Selected = 4,
        Rejected = 5,
        Withdrawn = 6
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Processing = 2,
        Completed = 3,
        Failed = 4,
        Refunded = 5
    }

    public enum PaymentMethod
    {
        BankTransfer = 1,
        DigitalWallet = 2,
        CreditCard = 3,
        UPI = 4
    }

    public enum TransactionType
    {
        PaymentReceived = 1,
        Withdrawal = 2,
        Refund = 3,
        FeeDeduction = 4
    }

    public enum MessageStatus
    {
        Sent = 1,
        Delivered = 2,
        Read = 3
    }

    public enum NotificationType
    {
        JobApplication = 1,
        Payment = 2,
        Message = 3,
        System = 4,
        Reminder = 5,
        JobStatus
    }

    public enum TransactionStatus
    {
        Success = 1,
        Pending = 2,
        Failed = 3
    }

    public enum ReportType
    {
        UserGrowth = 1,
        JobTrends = 2,
        PaymentSummary = 3,
        UsageAnalytics = 4
    }

    public enum ProficiencyLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3,
        Expert = 4
    }

    public enum PayType
    {
        Hourly = 1,
        Daily = 2,
        Weekly = 3,
        Monthly = 4,
        Fixed = 5
    }

    public enum WithdrawalStatus
    {
        Pending = 1,
        Processing = 2,
        Completed = 3,
        Rejected = 4
    }

    public enum RatingType
    {
        StudentToEmployer = 1,
        EmployerToStudent = 2
    }

    public enum OTPPurpose
    {
        Registration = 1,
        Login = 2,
        PasswordReset = 3,
        PhoneVerification = 4,
        EmailVerification = 5
    }

    public enum AuditAction
    {
        Create = 1,
        Update = 2,
        Delete = 3,
        Login = 4,
        Logout = 5
    }

    public enum DataType
    {
        String = 1,
        Integer = 2,
        Boolean = 3,
        Decimal = 4,
        DateTime = 5,
        Json = 6
    }
}