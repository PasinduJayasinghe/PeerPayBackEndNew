using Domain.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class PeerPayDbContext : DbContext
    {
        public PeerPayDbContext(DbContextOptions<PeerPayDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<StudentSkill> StudentSkills { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Earnings> Earnings { get; set; }
        public DbSet<WithdrawalRequest> WithdrawalRequests { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<OTPVerification> OTPVerifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Phone).IsUnique();

                entity.HasOne(e => e.Profile)
                    .WithOne(p => p.User)
                    .HasForeignKey<Profile>(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Notifications)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Sessions)
                    .WithOne(s => s.User)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Student Configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);
                entity.Property(e => e.StudentId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.University).HasMaxLength(255).IsRequired(false);
                entity.Property(e => e.Course).HasMaxLength(255).IsRequired(false);
                entity.Property(e => e.AcademicVerificationStatus).IsRequired(false);
                entity.Property(e => e.CvUrl).IsRequired(false);
                entity.Property(e => e.Rating).HasColumnType("decimal(3,2)");
                entity.Property(e => e.TotalEarnings).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<Student>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Skills)
                    .WithOne(s => s.Student)
                    .HasForeignKey(s => s.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Note: JobApplication now relates directly to User, not Student
                // entity.HasMany(e => e.Applications)
                //     .WithOne(a => a.Student)
                //     .HasForeignKey(a => a.StudentId)
                //     .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Earnings)
                    .WithOne(ea => ea.Student)
                    .HasForeignKey<Earnings>(ea => ea.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Employer Configuration
            modelBuilder.Entity<Employer>(entity =>
            {
                entity.HasKey(e => e.EmployerId);
                entity.Property(e => e.EmployerId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CompanyName).HasMaxLength(255).IsRequired();
                entity.Property(e => e.CompanyType).HasMaxLength(100);
                entity.Property(e => e.ContactPerson).HasMaxLength(200);
                entity.Property(e => e.Rating).HasColumnType("decimal(3,2)");

                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<Employer>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Jobs)
                    .WithOne(j => j.Employer)
                    .HasForeignKey(j => j.EmployerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Payments)
                    .WithOne(p => p.Employer)
                    .HasForeignKey(p => p.EmployerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Admin Configuration
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminId);
                entity.Property(e => e.AdminId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Role).HasMaxLength(100);

                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<Admin>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.GeneratedReports)
                    .WithOne(r => r.GeneratedByAdmin)
                    .HasForeignKey(r => r.GeneratedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Profile Configuration
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.ProfileId);
                entity.Property(e => e.ProfileId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(500);
            });

            // StudentSkill Configuration
            modelBuilder.Entity<StudentSkill>(entity =>
            {
                entity.HasKey(e => e.SkillId);
                entity.Property(e => e.SkillId).HasMaxLength(50);
                entity.Property(e => e.StudentId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.SkillName).HasMaxLength(100).IsRequired();
            });

            // JobCategory Configuration
            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryId).HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();

                entity.HasIndex(e => e.Name).IsUnique();

                entity.HasMany(e => e.Jobs)
                    .WithOne(j => j.Category)
                    .HasForeignKey(j => j.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Job Configuration
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobId);
                entity.Property(e => e.JobId).HasMaxLength(50);
                entity.Property(e => e.EmployerId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CategoryId).HasMaxLength(50);
                entity.Property(e => e.Title).HasMaxLength(300).IsRequired();
                entity.Property(e => e.PayAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Location).HasMaxLength(255);

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Jobs)
                    .HasForeignKey(e => e.CategoryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.Applications)
                    .WithOne(a => a.Job)
                    .HasForeignKey(a => a.JobId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Payment)
                    .WithOne(p => p.Job)
                    .HasForeignKey<Payment>(p => p.JobId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Ratings)
                    .WithOne(r => r.Job)
                    .HasForeignKey(r => r.JobId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // JobApplication Configuration
            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId);
                entity.Property(e => e.ApplicationId).HasMaxLength(50);
                entity.Property(e => e.JobId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.UpdatedBy).HasMaxLength(50);
                entity.Property(e => e.CoverLetter).HasMaxLength(2000);
                entity.Property(e => e.EmployerNotes).HasMaxLength(1000);

                // Convert string array to JSON for database storage
                entity.Property(e => e.Attachments)
                    .HasConversion(
                        v => string.Join(',', v ?? Array.Empty<string>()),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    );

                // Foreign key relationships
                entity.HasOne(e => e.Job)
                    .WithMany(j => j.Applications)
                    .HasForeignKey(e => e.JobId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Unique constraint: user can only apply once per job
                entity.HasIndex(e => new { e.JobId, e.UserId }).IsUnique();
            });

            // Payment Configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);
                entity.Property(e => e.PaymentId).HasMaxLength(50);
                entity.Property(e => e.JobId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.EmployerId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.StudentId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TransactionId).HasMaxLength(100);

                entity.HasMany(e => e.Transactions)
                    .WithOne(t => t.Payment)
                    .HasForeignKey(t => t.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Transaction Configuration
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);
                entity.Property(e => e.TransactionId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PaymentId).HasMaxLength(50);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Earnings Configuration
            modelBuilder.Entity<Earnings>(entity =>
            {
                entity.HasKey(e => e.EarningsId);
                entity.Property(e => e.EarningsId).HasMaxLength(50);
                entity.Property(e => e.StudentId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.TotalEarnings).HasColumnType("decimal(18,2)");
                entity.Property(e => e.AvailableBalance).HasColumnType("decimal(18,2)");
                entity.Property(e => e.WithdrawnAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PendingAmount).HasColumnType("decimal(18,2)");
            });

            // WithdrawalRequest Configuration
            modelBuilder.Entity<WithdrawalRequest>(entity =>
            {
                entity.HasKey(e => e.WithdrawalId);
                entity.Property(e => e.WithdrawalId).HasMaxLength(50);
                entity.Property(e => e.StudentId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ProcessedBy).HasMaxLength(50);

                entity.HasOne(e => e.Student)
                    .WithMany()
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Conversation Configuration
            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => e.ConversationId);
                entity.Property(e => e.ConversationId).HasMaxLength(50);
                entity.Property(e => e.Participant1Id).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Participant2Id).HasMaxLength(50).IsRequired();
                entity.Property(e => e.JobId).HasMaxLength(50);

                entity.HasOne(e => e.Participant1)
                    .WithMany()
                    .HasForeignKey(e => e.Participant1Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Participant2)
                    .WithMany()
                    .HasForeignKey(e => e.Participant2Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Job)
                    .WithMany()
                    .HasForeignKey(e => e.JobId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Messages)
                    .WithOne(m => m.Conversation)
                    .HasForeignKey(m => m.ConversationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Message Configuration
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.MessageId);
                entity.Property(e => e.MessageId).HasMaxLength(50);
                entity.Property(e => e.ConversationId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.SenderId).HasMaxLength(50).IsRequired();

                entity.HasOne(e => e.Sender)
                    .WithMany()
                    .HasForeignKey(e => e.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Notification Configuration
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);
                entity.Property(e => e.NotificationId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
                entity.Property(e => e.ActionUrl).HasMaxLength(500);
            });

            // Rating Configuration
            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.RatingId);
                entity.Property(e => e.RatingId).HasMaxLength(50);
                entity.Property(e => e.JobId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.RaterId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.RatedUserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.RatingValue).IsRequired();

                entity.HasOne(e => e.Rater)
                    .WithMany()
                    .HasForeignKey(e => e.RaterId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.RatedUser)
                    .WithMany()
                    .HasForeignKey(e => e.RatedUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.JobId, e.RaterId, e.RatedUserId }).IsUnique();
            });

            // Report Configuration
            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.ReportId);
                entity.Property(e => e.ReportId).HasMaxLength(50);
                entity.Property(e => e.GeneratedBy).HasMaxLength(50).IsRequired();
                entity.Property(e => e.FileUrl).HasMaxLength(500);
            });

            // SystemConfig Configuration
            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.HasKey(e => e.ConfigId);
                entity.Property(e => e.ConfigId).HasMaxLength(50);
                entity.Property(e => e.ConfigKey).HasMaxLength(200).IsRequired();
                entity.Property(e => e.ConfigValue).IsRequired();
                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.HasIndex(e => e.ConfigKey).IsUnique();
            });

            // AuditLog Configuration
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.AuditId);
                entity.Property(e => e.AuditId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.EntityType).HasMaxLength(100).IsRequired();
                entity.Property(e => e.EntityId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.IpAddress).HasMaxLength(50);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // FileUpload Configuration
            modelBuilder.Entity<FileUpload>(entity =>
            {
                entity.HasKey(e => e.FileId);
                entity.Property(e => e.FileId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.EntityType).HasMaxLength(100);
                entity.Property(e => e.EntityId).HasMaxLength(50);
                entity.Property(e => e.OriginalName).HasMaxLength(500).IsRequired();
                entity.Property(e => e.FilePath).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.FileType).HasMaxLength(100);
                entity.Property(e => e.HashValue).HasMaxLength(100);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // UserSession Configuration
            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasKey(e => e.SessionId);
                entity.Property(e => e.SessionId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.DeviceType).HasMaxLength(50);
                entity.Property(e => e.IpAddress).HasMaxLength(50);
                entity.Property(e => e.UserAgent).HasMaxLength(500);
            });

            // OTPVerification Configuration
            modelBuilder.Entity<OTPVerification>(entity =>
            {
                entity.HasKey(e => e.OtpId);
                entity.Property(e => e.OtpId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired(false); // Nullable for registration OTPs
                entity.Property(e => e.OtpCode).HasMaxLength(10).IsRequired();
                entity.Property(e => e.ContactMethod).HasMaxLength(255);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired(false); // Nullable relationship
            });
        }
    }
}
