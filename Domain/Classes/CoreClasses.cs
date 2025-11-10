using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DataType = Domain.Enums.DataType;
using TransactionStatus = Domain.Enums.TransactionStatus;

namespace Domain.Classes
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public UserStatus Status { get; set; }
        public bool IsVerified { get; set; }

        // Navigation properties
        public Profile Profile { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<UserSession> Sessions { get; set; }
    }

    public class Student
    {
        public string StudentId { get; set; }
        public string UserId { get; set; }
        public string University { get; set; }
        public string Course { get; set; }
        public int YearOfStudy { get; set; }
        public string AcademicVerificationStatus { get; set; }
        public decimal Rating { get; set; }
        public int CompletedJobs { get; set; }
        public decimal TotalEarnings { get; set; }
        public string CvUrl { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<StudentSkill> Skills { get; set; }
        public ICollection<JobApplication> Applications { get; set; }
        public Earnings Earnings { get; set; }
    }

    public class Employer
    {
        public string EmployerId { get; set; }
        public string UserId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public string VerificationStatus { get; set; }
        public decimal Rating { get; set; }
        public int JobsPosted { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }

    public class Admin
    {
        public string AdminId { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public string[] Permissions { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<Report> GeneratedReports { get; set; }
    }

    public class Profile
    {
        public string ProfileId { get; set; }
        public string UserId { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string[] Documents { get; set; }

        // Navigation property
        public User User { get; set; }
    }

    public class StudentSkill
    {
        public string SkillId { get; set; }
        public string StudentId { get; set; }
        public string SkillName { get; set; }
        public ProficiencyLevel ProficiencyLevel { get; set; }

        // Navigation property
        public Student Student { get; set; }
    }

    public class JobCategory
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public ICollection<Job> Jobs { get; set; }
    }

    public class Job
    {
        public string JobId { get; set; }
        public string EmployerId { get; set; }
        public string? CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PayAmount { get; set; }
        public PayType PayType { get; set; }
        public int DurationDays { get; set; }
        public string[] RequiredSkills { get; set; }
        public string[] Attachments { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime Deadline { get; set; }
        public JobStatus Status { get; set; }
        public string Location { get; set; }
        public JobType JobType { get; set; }
        public int MaxApplicants { get; set; }

        // Navigation properties
        public Employer Employer { get; set; }
        public JobCategory? Category { get; set; }
        public ICollection<JobApplication> Applications { get; set; }
        public Payment Payment { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }

    public class JobApplication
    {
        public string ApplicationId { get; set; }
        public string JobId { get; set; }
        public string StudentId { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public string CoverLetter { get; set; }
        public string[] Attachments { get; set; }
        public DateTime StatusUpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string EmployerNotes { get; set; }

        // Navigation properties
        public Job Job { get; set; }
        public Student Student { get; set; }
    }

    public class Payment
    {
        public string PaymentId { get; set; }
        public string JobId { get; set; }
        public string EmployerId { get; set; }
        public string StudentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string TransactionId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string GatewayResponse { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public Job Job { get; set; }
        public Employer Employer { get; set; }
        public Student Student { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }

    public class Transaction
    {
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public string PaymentId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus Status { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Payment Payment { get; set; }
    }

    public class Earnings
    {
        public string EarningsId { get; set; }
        public string StudentId { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal WithdrawnAmount { get; set; }
        public decimal PendingAmount { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation property
        public Student Student { get; set; }
    }

    public class WithdrawalRequest
    {
        public string WithdrawalId { get; set; }
        public string StudentId { get; set; }
        public decimal Amount { get; set; }
        public WithdrawalStatus Status { get; set; }
        public string BankDetails { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string ProcessedBy { get; set; }
        public string Notes { get; set; }

        // Navigation property
        public Student Student { get; set; }
    }

    public class Conversation
    {
        public string ConversationId { get; set; }
        public string Participant1Id { get; set; }
        public string Participant2Id { get; set; }
        public string JobId { get; set; }
        public DateTime LastMessageAt { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public User Participant1 { get; set; }
        public User Participant2 { get; set; }
        public Job Job { get; set; }
        public ICollection<Message> Messages { get; set; }
    }

    public class Message
    {
        public string MessageId { get; set; }
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public string[] Attachments { get; set; }
        public DateTime Timestamp { get; set; }
        public MessageStatus Status { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        // Navigation properties
        public Conversation Conversation { get; set; }
        public User Sender { get; set; }
    }

    public class Notification
    {
        public string NotificationId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string ActionUrl { get; set; }
        public string Metadata { get; set; }
        public DateTime? ExpiresAt { get; set; }

        // Navigation property
        public User User { get; set; }
    }

    public class Rating
    {
        public string RatingId { get; set; }
        public string JobId { get; set; }
        public string RaterId { get; set; }
        public string RatedUserId { get; set; }
        public int RatingValue { get; set; }
        public string Review { get; set; }
        public RatingType RatingType { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Job Job { get; set; }
        public User Rater { get; set; }
        public User RatedUser { get; set; }
    }

    public class Report
    {
        public string ReportId { get; set; }
        public string GeneratedBy { get; set; }
        public ReportType ReportType { get; set; }
        public string Parameters { get; set; }
        public string Data { get; set; }
        public string FileUrl { get; set; }
        public DateTime GeneratedDate { get; set; }
        public DateTime? ExpiresAt { get; set; }

        // Navigation property
        public Admin GeneratedByAdmin { get; set; }
    }

    public class SystemConfig
    {
        public string ConfigId { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public DataType DataType { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class AuditLog
    {
        public string AuditId { get; set; }
        public string UserId { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public AuditAction Action { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation property
        public User User { get; set; }
    }

    public class FileUpload
    {
        public string FileId { get; set; }
        public string UserId { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string OriginalName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string HashValue { get; set; }
        public DateTime UploadedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        // Navigation property
        public User User { get; set; }
    }

    public class UserSession
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string DeviceType { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsActive { get; set; }

        // Navigation property
        public User User { get; set; }
    }

    public class OTPVerification
    {
        public string OtpId { get; set; }
        public string UserId { get; set; }
        public string OtpCode { get; set; }
        public OTPPurpose Purpose { get; set; }
        public string ContactMethod { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiresAt { get; set; }
        public int Attempts { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
