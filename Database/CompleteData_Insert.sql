-- =============================================
-- PeerPay Complete Mock Data Insert Script
-- Description: Comprehensive mock data for all database tables
-- Date: October 26, 2025
-- =============================================

USE [PeerPayDB]; -- Change this to your actual database name
GO

SET NOCOUNT ON;
GO

-- =============================================
-- 1. Insert Job Categories
-- =============================================
PRINT 'Inserting Job Categories...';

INSERT INTO JobCategories (CategoryId, Name, Description, IsActive)
VALUES 
    ('CAT001', 'Web Development', 'Full-stack, frontend, and backend web development projects', 1),
    ('CAT002', 'Mobile App Development', 'iOS, Android, and cross-platform mobile applications', 1),
    ('CAT003', 'Graphic Design', 'Logo design, branding, UI/UX design, and visual content', 1),
    ('CAT004', 'Content Writing', 'Blog posts, articles, copywriting, and technical writing', 1),
    ('CAT005', 'Video Editing', 'Video production, editing, animation, and post-production', 1),
    ('CAT006', 'Data Entry', 'Data entry, data processing, and administrative tasks', 1),
    ('CAT007', 'Digital Marketing', 'SEO, social media marketing, email campaigns, and ads', 1),
    ('CAT008', 'Translation', 'Language translation and localization services', 1),
    ('CAT009', 'Photography', 'Product photography, event coverage, and photo editing', 1),
    ('CAT010', 'Tutoring', 'Academic tutoring, skill training, and educational support', 1),
    ('CAT011', 'Data Analysis', 'Data science, analytics, visualization, and reporting', 1),
    ('CAT012', 'Voice Over', 'Voice acting, narration, and audio production', 1);

PRINT 'Job Categories inserted: 12';
GO

-- =============================================
-- 2. Insert Admin Users
-- =============================================
PRINT 'Inserting Admin Users...';

-- Admin User 1 - Super Admin
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_ADMIN_001', 'admin@peerpay.lk', '+94112650301', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 2, 'System Administrator', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Admins (AdminId, UserId, Role, Permissions)
VALUES ('ADM001', 'USR_ADMIN_001', 'SuperAdmin', 'ALL');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_ADMIN_001', 'USR_ADMIN_001', 'System administrator managing PeerPay platform.', 'Colombo 07', '/images/admins/admin1.jpg', '[]');

-- Admin User 2 - Content Moderator
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_ADMIN_002', 'moderator@peerpay.lk', '+94112650302', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 2, 'Content Moderator', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Admins (AdminId, UserId, Role, Permissions)
VALUES ('ADM002', 'USR_ADMIN_002', 'Moderator', 'VIEW_USERS,MODERATE_CONTENT,VIEW_REPORTS');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_ADMIN_002', 'USR_ADMIN_002', 'Content moderator ensuring quality standards.', 'Colombo 03', '/images/admins/admin2.jpg', '[]');

-- Admin User 3 - Support Agent
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_ADMIN_003', 'support@peerpay.lk', '+94112650303', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 2, 'Support Agent', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Admins (AdminId, UserId, Role, Permissions)
VALUES ('ADM003', 'USR_ADMIN_003', 'Support', 'VIEW_USERS,VIEW_TICKETS,RESPOND_TICKETS');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_ADMIN_003', 'USR_ADMIN_003', 'Customer support specialist.', 'Colombo 05', '/images/admins/admin3.jpg', '[]');

PRINT 'Admin Users inserted: 3';
GO

-- =============================================
-- 3. Insert Student Users
-- =============================================
PRINT 'Inserting Student Users...';

-- Student 1
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_STU_001', 'kasun.perera@student.uom.lk', '+94771234567', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Kasun Perera', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES ('STU001', 'USR_STU_001', 'University of Moratuwa', 'BSc (Hons) in Information Technology', 2, 'Verified', 4.90, 47, 235000.00, '/files/cv/kasun_perera_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_STU_001', 'USR_STU_001', 'Passionate full-stack developer with expertise in React, Node.js, and TypeScript.', 'Moratuwa', '/images/profiles/kasun.jpg', '["student_id.pdf","transcript.pdf"]');

-- Student 2
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_STU_002', 'nimali.silva@ucsc.cmb.ac.lk', '+94772345678', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Nimali Silva', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES ('STU002', 'USR_STU_002', 'University of Colombo School of Computing', 'BSc in Computer Science', 3, 'Verified', 4.80, 38, 192000.00, '/files/cv/nimali_silva_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_STU_002', 'USR_STU_002', 'Creative UI/UX designer specializing in Figma and Adobe XD.', 'Colombo 07', '/images/profiles/nimali.jpg', '["student_id.pdf","portfolio.pdf"]');

-- Student 3
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_STU_003', 'ravindu.fernando@sliit.lk', '+94773456789', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Ravindu Fernando', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES ('STU003', 'USR_STU_003', 'Sri Lanka Institute of Information Technology', 'BSc (Hons) in IT specializing in Data Science', 2, 'Verified', 4.90, 52, 286000.00, '/files/cv/ravindu_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_STU_003', 'USR_STU_003', 'Data enthusiast skilled in Python, machine learning, and visualization.', 'Malabe', '/images/profiles/ravindu.jpg', '["student_id.pdf","certs.pdf"]');

-- Student 4
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_STU_004', 'thisara.j@pdn.ac.lk', '+94774567890', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Thisara Jayasinghe', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES ('STU004', 'USR_STU_004', 'University of Peradeniya', 'BA (Hons) in English', 3, 'Verified', 4.70, 65, 162500.00, '/files/cv/thisara_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_STU_004', 'USR_STU_004', 'Professional content writer with SEO expertise.', 'Kandy', '/images/profiles/thisara.jpg', '["student_id.pdf"]');

-- Student 5
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_STU_005', 'dinuka.w@uom.lk', '+94775678901', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Dinuka Wickramasinghe', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES ('STU005', 'USR_STU_005', 'University of Moratuwa', 'BSc in Multimedia and Web Technology', 2, 'Verified', 4.85, 31, 186000.00, '/files/cv/dinuka_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_STU_005', 'USR_STU_005', 'Creative video editor proficient in Premiere Pro and After Effects.', 'Katubedda', '/images/profiles/dinuka.jpg', '["student_id.pdf"]');

PRINT 'Student Users inserted: 5';
GO

-- =============================================
-- 4. Insert Employer Users
-- =============================================
PRINT 'Inserting Employer Users...';

-- Employer 1
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_EMP_001', 'hr@techstartup.lk', '+94112223344', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'Tech Startup Lanka', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES ('EMP001', 'USR_EMP_001', 'Tech Startup Lanka', 'Software Company', 'Innovative tech startup building next-generation applications.', 'Nimal Fernando', 'Verified', 4.80, 15);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_EMP_001', 'USR_EMP_001', 'Fast-growing tech startup.', 'Colombo 07', '/images/companies/tech.jpg', '["reg.pdf"]');

-- Employer 2
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_EMP_002', 'contact@digitalhub.lk', '+94112334455', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'Digital Hub Agency', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES ('EMP002', 'USR_EMP_002', 'Digital Hub Agency', 'Marketing Agency', 'Full-service digital marketing agency.', 'Sanduni Perera', 'Verified', 4.70, 22);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_EMP_002', 'USR_EMP_002', 'Award-winning marketing agency.', 'Colombo 03', '/images/companies/digital.jpg', '["reg.pdf"]');

-- Employer 3
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES ('USR_EMP_003', 'hiring@shopceylon.lk', '+94112445566', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'ShopCeylon', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES ('EMP003', 'USR_EMP_003', 'ShopCeylon', 'E-commerce', 'Leading e-commerce platform.', 'Chaminda Rathnayake', 'Verified', 4.85, 18);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES ('PROF_EMP_003', 'USR_EMP_003', 'Fastest-growing e-commerce.', 'Colombo 05', '/images/companies/shop.jpg', '["reg.pdf"]');

PRINT 'Employer Users inserted: 3';
GO

-- =============================================
-- 5. Insert Student Skills
-- =============================================
PRINT 'Inserting Student Skills...';

INSERT INTO StudentSkills (SkillId, StudentId, SkillName, ProficiencyLevel) VALUES
('SKILL001', 'STU001', 'React', 3),
('SKILL002', 'STU001', 'Node.js', 3),
('SKILL003', 'STU001', 'TypeScript', 2),
('SKILL004', 'STU001', 'MongoDB', 2),
('SKILL005', 'STU002', 'UI/UX Design', 3),
('SKILL006', 'STU002', 'Figma', 3),
('SKILL007', 'STU002', 'Adobe XD', 2),
('SKILL008', 'STU003', 'Python', 3),
('SKILL009', 'STU003', 'Data Analysis', 3),
('SKILL010', 'STU003', 'Machine Learning', 2),
('SKILL011', 'STU004', 'Content Writing', 3),
('SKILL012', 'STU004', 'SEO', 2),
('SKILL013', 'STU005', 'Video Editing', 3),
('SKILL014', 'STU005', 'Adobe Premiere Pro', 3),
('SKILL015', 'STU005', 'After Effects', 2);

PRINT 'Student Skills inserted: 15';
GO

-- =============================================
-- 6. Insert Jobs
-- =============================================
PRINT 'Inserting Jobs...';

-- Job 1 - Active
INSERT INTO Jobs (JobId, EmployerId, CategoryId, Title, Description, PayAmount, PayType, DurationDays, RequiredSkills, Attachments, PostedDate, Deadline, Status, Location, JobType, MaxApplicants)
VALUES ('JOB001', 'EMP001', 'CAT001', 'React Developer for E-commerce Platform', 
'We need an experienced React developer to build a modern e-commerce frontend with shopping cart, payment integration, and admin dashboard.',
35000.00, 0, 60, '["React","TypeScript","Redux","REST API"]', '[]', 
DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, 25, GETDATE()), 0, 'Remote', 0, 10);

-- Job 2 - Active
INSERT INTO Jobs (JobId, EmployerId, CategoryId, Title, Description, PayAmount, PayType, DurationDays, RequiredSkills, Attachments, PostedDate, Deadline, Status, Location, JobType, MaxApplicants)
VALUES ('JOB002', 'EMP002', 'CAT003', 'Brand Identity Design Package', 
'Looking for a creative designer to create complete brand identity including logo, color palette, typography, and brand guidelines.',
25000.00, 0, 21, '["Adobe Illustrator","Photoshop","Brand Design"]', '[]',
DATEADD(DAY, -3, GETDATE()), DATEADD(DAY, 18, GETDATE()), 0, 'Colombo', 1, 5);

-- Job 3 - Active
INSERT INTO Jobs (JobId, EmployerId, CategoryId, Title, Description, PayAmount, PayType, DurationDays, RequiredSkills, Attachments, PostedDate, Deadline, Status, Location, JobType, MaxApplicants)
VALUES ('JOB003', 'EMP003', 'CAT004', 'SEO Blog Content Writer', 
'Need talented writer for 20 SEO-optimized blog posts about e-commerce and online shopping trends.',
18000.00, 0, 30, '["Content Writing","SEO","Research"]', '[]',
DATEADD(DAY, -7, GETDATE()), DATEADD(DAY, 23, GETDATE()), 0, 'Remote', 0, 8);

-- Job 4 - Completed
INSERT INTO Jobs (JobId, EmployerId, CategoryId, Title, Description, PayAmount, PayType, DurationDays, RequiredSkills, Attachments, PostedDate, Deadline, Status, Location, JobType, MaxApplicants)
VALUES ('JOB004', 'EMP001', 'CAT005', 'Product Demo Video Creation', 
'Create engaging 2-minute product demo video with animations and professional voiceover.',
22000.00, 0, 14, '["Video Editing","After Effects","Animation"]', '[]',
DATEADD(DAY, -45, GETDATE()), DATEADD(DAY, -15, GETDATE()), 2, 'Remote', 0, 5);

-- Job 5 - Closed
INSERT INTO Jobs (JobId, EmployerId, CategoryId, Title, Description, PayAmount, PayType, DurationDays, RequiredSkills, Attachments, PostedDate, Deadline, Status, Location, JobType, MaxApplicants)
VALUES ('JOB005', 'EMP002', 'CAT007', 'Social Media Marketing Campaign', 
'Manage Instagram and Facebook accounts for 1 month, create content calendar and engaging posts.',
15000.00, 0, 30, '["Social Media Marketing","Content Creation","Canva"]', '[]',
DATEADD(DAY, -60, GETDATE()), DATEADD(DAY, -30, GETDATE()), 3, 'Remote', 0, 3);

-- Job 6 - Active
INSERT INTO Jobs (JobId, EmployerId, CategoryId, Title, Description, PayAmount, PayType, DurationDays, RequiredSkills, Attachments, PostedDate, Deadline, Status, Location, JobType, MaxApplicants)
VALUES ('JOB006', 'EMP003', 'CAT011', 'Data Analysis Dashboard', 
'Build interactive dashboard using Python and Tableau for sales data visualization and reporting.',
40000.00, 0, 45, '["Python","Tableau","Data Analysis","SQL"]', '[]',
DATEADD(DAY, -2, GETDATE()), DATEADD(DAY, 28, GETDATE()), 0, 'Remote', 0, 6);

PRINT 'Jobs inserted: 6';
GO

-- =============================================
-- 7. Insert Job Applications
-- =============================================
PRINT 'Inserting Job Applications...';

-- Applications for JOB001
INSERT INTO JobApplications (ApplicationId, JobId, StudentId, AppliedDate, Status, CoverLetter, Attachments, StatusUpdatedAt, UpdatedBy, EmployerNotes)
VALUES ('APP001', 'JOB001', 'STU001', DATEADD(DAY, -4, GETDATE()), 1, 
'I am excited to apply for this position. With 2 years of React experience and 47 completed projects, I can deliver high-quality work.',
'["portfolio.pdf"]', DATEADD(DAY, -3, GETDATE()), 'EMP001', 'Strong portfolio');

INSERT INTO JobApplications (ApplicationId, JobId, StudentId, AppliedDate, Status, CoverLetter, Attachments, StatusUpdatedAt, UpdatedBy, EmployerNotes)
VALUES ('APP002', 'JOB001', 'STU003', DATEADD(DAY, -4, GETDATE()), 0,
'Hello, I have experience with React and would love to work on this project.',
'[]', DATEADD(DAY, -4, GETDATE()), '', '');

-- Applications for JOB002
INSERT INTO JobApplications (ApplicationId, JobId, StudentId, AppliedDate, Status, CoverLetter, Attachments, StatusUpdatedAt, UpdatedBy, EmployerNotes)
VALUES ('APP003', 'JOB002', 'STU002', DATEADD(DAY, -2, GETDATE()), 0,
'I am a UI/UX designer with expertise in brand identity design. Check out my portfolio!',
'["portfolio.pdf","samples.zip"]', DATEADD(DAY, -2, GETDATE()), '', '');

-- Applications for JOB003
INSERT INTO JobApplications (ApplicationId, JobId, StudentId, AppliedDate, Status, CoverLetter, Attachments, StatusUpdatedAt, UpdatedBy, EmployerNotes)
VALUES ('APP004', 'JOB003', 'STU004', DATEADD(DAY, -6, GETDATE()), 1,
'With 65 completed writing projects and SEO expertise, I can create engaging, optimized content.',
'["writing_samples.pdf"]', DATEADD(DAY, -5, GETDATE()), 'EMP003', 'Excellent samples');

-- Applications for JOB004 (Completed)
INSERT INTO JobApplications (ApplicationId, JobId, StudentId, AppliedDate, Status, CoverLetter, Attachments, StatusUpdatedAt, UpdatedBy, EmployerNotes)
VALUES ('APP005', 'JOB004', 'STU005', DATEADD(DAY, -44, GETDATE()), 2,
'I specialize in video editing and have created numerous product demos.',
'["video_reel.mp4"]', DATEADD(DAY, -40, GETDATE()), 'EMP001', 'Completed successfully');

-- Applications for JOB006
INSERT INTO JobApplications (ApplicationId, JobId, StudentId, AppliedDate, Status, CoverLetter, Attachments, StatusUpdatedAt, UpdatedBy, EmployerNotes)
VALUES ('APP006', 'JOB006', 'STU003', DATEADD(DAY, -1, GETDATE()), 0,
'Data analysis is my specialty. I have experience with Python, Tableau, and building dashboards.',
'["portfolio.pdf","dashboard_examples.pdf"]', DATEADD(DAY, -1, GETDATE()), '', '');

PRINT 'Job Applications inserted: 6';
GO

-- =============================================
-- 8. Insert Conversations
-- =============================================
PRINT 'Inserting Conversations...';

INSERT INTO Conversations (ConversationId, Participant1Id, Participant2Id, JobId, LastMessageAt, IsActive)
VALUES ('CONV001', 'USR_EMP_001', 'USR_STU_001', 'JOB001', DATEADD(HOUR, -2, GETDATE()), 1);

INSERT INTO Conversations (ConversationId, Participant1Id, Participant2Id, JobId, LastMessageAt, IsActive)
VALUES ('CONV002', 'USR_EMP_003', 'USR_STU_004', 'JOB003', DATEADD(HOUR, -5, GETDATE()), 1);

INSERT INTO Conversations (ConversationId, Participant1Id, Participant2Id, JobId, LastMessageAt, IsActive)
VALUES ('CONV003', 'USR_EMP_001', 'USR_STU_005', 'JOB004', DATEADD(DAY, -20, GETDATE()), 0);

PRINT 'Conversations inserted: 3';
GO

-- =============================================
-- 9. Insert Messages
-- =============================================
PRINT 'Inserting Messages...';

-- Messages in CONV001
INSERT INTO Messages (MessageId, ConversationId, SenderId, Content, Attachments, Timestamp, Status, IsRead, ReadAt)
VALUES ('MSG001', 'CONV001', 'USR_EMP_001', 'Hi Kasun, I reviewed your application and portfolio. When can you start?', '[]', 
DATEADD(HOUR, -3, GETDATE()), 1, 1, DATEADD(HOUR, -2, GETDATE()));

INSERT INTO Messages (MessageId, ConversationId, SenderId, Content, Attachments, Timestamp, Status, IsRead, ReadAt)
VALUES ('MSG002', 'CONV001', 'USR_STU_001', 'Thank you! I can start next week. Should we discuss the project requirements?', '[]',
DATEADD(HOUR, -2, GETDATE()), 1, 1, DATEADD(HOUR, -2, GETDATE()));

INSERT INTO Messages (MessageId, ConversationId, SenderId, Content, Attachments, Timestamp, Status, IsRead, ReadAt)
VALUES ('MSG003', 'CONV001', 'USR_EMP_001', 'Perfect! Let me send you the detailed specifications.', '["specs.pdf"]',
DATEADD(HOUR, -2, GETDATE()), 1, 0, NULL);

-- Messages in CONV002
INSERT INTO Messages (MessageId, ConversationId, SenderId, Content, Attachments, Timestamp, Status, IsRead, ReadAt)
VALUES ('MSG004', 'CONV002', 'USR_EMP_003', 'Your writing samples are impressive! Are you available to start immediately?', '[]',
DATEADD(HOUR, -6, GETDATE()), 1, 1, DATEADD(HOUR, -5, GETDATE()));

INSERT INTO Messages (MessageId, ConversationId, SenderId, Content, Attachments, Timestamp, Status, IsRead, ReadAt)
VALUES ('MSG005', 'CONV002', 'USR_STU_004', 'Yes, I am ready to start! What topics should I focus on?', '[]',
DATEADD(HOUR, -5, GETDATE()), 1, 1, DATEADD(HOUR, -5, GETDATE()));

PRINT 'Messages inserted: 5';
GO

-- =============================================
-- 10. Insert Payments
-- =============================================
PRINT 'Inserting Payments...';

-- Payment for completed job
INSERT INTO Payments (PaymentId, JobId, EmployerId, StudentId, Amount, Status, CreatedDate, CompletedDate, TransactionId, PaymentMethod, GatewayResponse, Notes)
VALUES ('PAY001', 'JOB004', 'EMP001', 'STU005', 22000.00, 2, 
DATEADD(DAY, -16, GETDATE()), DATEADD(DAY, -15, GETDATE()), 
'TXN_20251010_001', 0, '{"status":"success","gateway":"stripe"}', 'Payment completed successfully');

-- Payment in escrow
INSERT INTO Payments (PaymentId, JobId, EmployerId, StudentId, Amount, Status, CreatedDate, CompletedDate, TransactionId, PaymentMethod, GatewayResponse, Notes)
VALUES ('PAY002', 'JOB001', 'EMP001', 'STU001', 35000.00, 1,
DATEADD(DAY, -3, GETDATE()), NULL,
'TXN_20251023_002', 0, '{"status":"pending"}', 'Held in escrow');

PRINT 'Payments inserted: 2';
GO

-- =============================================
-- 11. Insert Ratings
-- =============================================
PRINT 'Inserting Ratings...';

-- Employer rates Student
INSERT INTO Ratings (RatingId, JobId, RaterId, RatedUserId, RatingValue, Review, RatingType, IsPublic, CreatedAt, UpdatedAt)
VALUES ('RAT001', 'JOB004', 'USR_EMP_001', 'USR_STU_005', 5,
'Excellent work! Video quality exceeded expectations. Highly recommended.', 0, 1,
DATEADD(DAY, -14, GETDATE()), NULL);

-- Student rates Employer
INSERT INTO Ratings (RatingId, JobId, RaterId, RatedUserId, RatingValue, Review, RatingType, IsPublic, CreatedAt, UpdatedAt)
VALUES ('RAT002', 'JOB004', 'USR_STU_005', 'USR_EMP_001', 5,
'Great client! Clear requirements and prompt payment. Would work again.', 1, 1,
DATEADD(DAY, -14, GETDATE()), NULL);

PRINT 'Ratings inserted: 2';
GO

-- =============================================
-- 12. Insert Earnings
-- =============================================
PRINT 'Inserting Earnings...';

INSERT INTO Earnings (EarningsId, StudentId, TotalEarnings, AvailableBalance, WithdrawnAmount, PendingAmount, LastUpdated)
VALUES ('EARN001', 'STU001', 235000.00, 85000.00, 150000.00, 35000.00, GETDATE());

INSERT INTO Earnings (EarningsId, StudentId, TotalEarnings, AvailableBalance, WithdrawnAmount, PendingAmount, LastUpdated)
VALUES ('EARN002', 'STU002', 192000.00, 92000.00, 100000.00, 0, GETDATE());

INSERT INTO Earnings (EarningsId, StudentId, TotalEarnings, AvailableBalance, WithdrawnAmount, PendingAmount, LastUpdated)
VALUES ('EARN003', 'STU003', 286000.00, 126000.00, 160000.00, 0, GETDATE());

INSERT INTO Earnings (EarningsId, StudentId, TotalEarnings, AvailableBalance, WithdrawnAmount, PendingAmount, LastUpdated)
VALUES ('EARN004', 'STU004', 162500.00, 62500.00, 100000.00, 18000.00, GETDATE());

INSERT INTO Earnings (EarningsId, StudentId, TotalEarnings, AvailableBalance, WithdrawnAmount, PendingAmount, LastUpdated)
VALUES ('EARN005', 'STU005', 186000.00, 64000.00, 100000.00, 22000.00, GETDATE());

PRINT 'Earnings inserted: 5';
GO

-- =============================================
-- 13. Insert Notifications
-- =============================================
PRINT 'Inserting Notifications...';

INSERT INTO Notifications (NotificationId, UserId, Title, Content, Type, IsRead, CreatedAt, ReadAt, ActionUrl, Metadata, ExpiresAt)
VALUES ('NOT001', 'USR_STU_001', 'Application Accepted', 'Your application for React Developer position has been accepted!', 0, 1,
DATEADD(DAY, -3, GETDATE()), DATEADD(DAY, -3, GETDATE()), '/student/jobs/JOB001', '{"jobId":"JOB001"}', NULL);

INSERT INTO Notifications (NotificationId, UserId, Title, Content, Type, IsRead, CreatedAt, ReadAt, ActionUrl, Metadata, ExpiresAt)
VALUES ('NOT002', 'USR_STU_001', 'New Message', 'You have a new message from Tech Startup Lanka', 1, 0,
DATEADD(HOUR, -2, GETDATE()), NULL, '/messages/CONV001', '{"conversationId":"CONV001"}', NULL);

INSERT INTO Notifications (NotificationId, UserId, Title, Content, Type, IsRead, CreatedAt, ReadAt, ActionUrl, Metadata, ExpiresAt)
VALUES ('NOT003', 'USR_EMP_001', 'New Application', 'You received a new application for React Developer position', 0, 1,
DATEADD(DAY, -4, GETDATE()), DATEADD(DAY, -4, GETDATE()), '/employer/jobs/JOB001/applications', '{"jobId":"JOB001"}', NULL);

INSERT INTO Notifications (NotificationId, UserId, Title, Content, Type, IsRead, CreatedAt, ReadAt, ActionUrl, Metadata, ExpiresAt)
VALUES ('NOT004', 'USR_STU_005', 'Payment Received', 'You received Rs 22,000 for Product Demo Video Creation', 2, 1,
DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -15, GETDATE()), '/student/earnings', '{"paymentId":"PAY001"}', NULL);

PRINT 'Notifications inserted: 4';
GO

-- =============================================
-- 14. Insert Withdrawal Requests
-- =============================================
PRINT 'Inserting Withdrawal Requests...';

INSERT INTO WithdrawalRequests (WithdrawalId, StudentId, Amount, Status, BankDetails, RequestedDate, ProcessedDate, ProcessedBy, Notes)
VALUES ('WDR001', 'STU001', 50000.00, 2, 
'{"bankName":"Bank of Ceylon","accountNumber":"12345678","accountName":"Kasun Perera","branch":"Moratuwa"}',
DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -8, GETDATE()), 'ADM001', 'Processed successfully');

INSERT INTO WithdrawalRequests (WithdrawalId, StudentId, Amount, Status, BankDetails, RequestedDate, ProcessedDate, ProcessedBy, Notes)
VALUES ('WDR002', 'STU003', 60000.00, 1,
'{"bankName":"Commercial Bank","accountNumber":"87654321","accountName":"Ravindu Fernando","branch":"Malabe"}',
DATEADD(DAY, -2, GETDATE()), NULL, '', 'Under review');

INSERT INTO WithdrawalRequests (WithdrawalId, StudentId, Amount, Status, BankDetails, RequestedDate, ProcessedDate, ProcessedBy, Notes)
VALUES ('WDR003', 'STU004', 30000.00, 0,
'{"bankName":"Peoples Bank","accountNumber":"11223344","accountName":"Thisara Jayasinghe","branch":"Kandy"}',
DATEADD(HOUR, -3, GETDATE()), NULL, '', '');

PRINT 'Withdrawal Requests inserted: 3';
GO

-- =============================================
-- 15. Insert Audit Logs
-- =============================================
PRINT 'Inserting Audit Logs...';

INSERT INTO AuditLogs (AuditId, UserId, EntityType, EntityId, Action, OldValues, NewValues, IpAddress, UserAgent, Timestamp)
VALUES ('AUD001', 'USR_EMP_001', 'Job', 'JOB001', 0,
'', '{"title":"React Developer","status":"Active"}', '192.168.1.100', 'Mozilla/5.0', DATEADD(DAY, -5, GETDATE()));

INSERT INTO AuditLogs (AuditId, UserId, EntityType, EntityId, Action, OldValues, NewValues, IpAddress, UserAgent, Timestamp)
VALUES ('AUD002', 'USR_STU_001', 'Application', 'APP001', 0,
'', '{"jobId":"JOB001","status":"Pending"}', '192.168.1.101', 'Mozilla/5.0', DATEADD(DAY, -4, GETDATE()));

INSERT INTO AuditLogs (AuditId, UserId, EntityType, EntityId, Action, OldValues, NewValues, IpAddress, UserAgent, Timestamp)
VALUES ('AUD003', 'USR_EMP_001', 'Application', 'APP001', 1,
'{"status":"Pending"}', '{"status":"Accepted"}', '192.168.1.100', 'Mozilla/5.0', DATEADD(DAY, -3, GETDATE()));

PRINT 'Audit Logs inserted: 3';
GO

-- =============================================
-- 16. Insert User Sessions
-- =============================================
PRINT 'Inserting User Sessions...';

INSERT INTO UserSessions (SessionId, UserId, DeviceType, IpAddress, UserAgent, LastAccessed, ExpiresAt, IsActive)
VALUES ('SESS001', 'USR_STU_001', 'Desktop', '192.168.1.101', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64)', 
GETDATE(), DATEADD(DAY, 7, GETDATE()), 1);

INSERT INTO UserSessions (SessionId, UserId, DeviceType, IpAddress, UserAgent, LastAccessed, ExpiresAt, IsActive)
VALUES ('SESS002', 'USR_EMP_001', 'Desktop', '192.168.1.100', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64)',
GETDATE(), DATEADD(DAY, 7, GETDATE()), 1);

INSERT INTO UserSessions (SessionId, UserId, DeviceType, IpAddress, UserAgent, LastAccessed, ExpiresAt, IsActive)
VALUES ('SESS003', 'USR_STU_002', 'Mobile', '192.168.1.102', 'Mozilla/5.0 (iPhone; CPU iPhone OS 14_0)',
DATEADD(HOUR, -5, GETDATE()), DATEADD(DAY, 6, GETDATE()), 1);

PRINT 'User Sessions inserted: 3';
GO

-- =============================================
-- 17. Insert System Configs
-- =============================================
PRINT 'Inserting System Configs...';

INSERT INTO SystemConfigs (ConfigId, ConfigKey, ConfigValue, Description, DataType, IsActive, LastModified, ModifiedBy)
VALUES ('CFG001', 'PLATFORM_FEE_PERCENTAGE', '10', 'Platform fee percentage charged on transactions', 0, 1, GETDATE(), 'ADM001');

INSERT INTO SystemConfigs (ConfigId, ConfigKey, ConfigValue, Description, DataType, IsActive, LastModified, ModifiedBy)
VALUES ('CFG002', 'MIN_WITHDRAWAL_AMOUNT', '5000', 'Minimum amount for withdrawal in LKR', 0, 1, GETDATE(), 'ADM001');

INSERT INTO SystemConfigs (ConfigId, ConfigKey, ConfigValue, Description, DataType, IsActive, LastModified, ModifiedBy)
VALUES ('CFG003', 'MAX_JOB_APPLICATIONS', '50', 'Maximum applications allowed per job', 0, 1, GETDATE(), 'ADM001');

INSERT INTO SystemConfigs (ConfigId, ConfigKey, ConfigValue, Description, DataType, IsActive, LastModified, ModifiedBy)
VALUES ('CFG004', 'EMAIL_NOTIFICATIONS_ENABLED', 'true', 'Enable/disable email notifications', 1, 1, GETDATE(), 'ADM001');

INSERT INTO SystemConfigs (ConfigId, ConfigKey, ConfigValue, Description, DataType, IsActive, LastModified, ModifiedBy)
VALUES ('CFG005', 'ESCROW_HOLD_DAYS', '7', 'Number of days to hold payment in escrow', 0, 1, GETDATE(), 'ADM001');

PRINT 'System Configs inserted: 5';
GO

-- =============================================
-- 18. Insert File Uploads
-- =============================================
PRINT 'Inserting File Uploads...';

INSERT INTO FileUploads (FileId, UserId, EntityType, EntityId, OriginalName, FilePath, FileType, FileSize, HashValue, UploadedAt, ExpiresAt)
VALUES ('FILE001', 'USR_STU_001', 'Profile', 'PROF_STU_001', 'kasun_cv.pdf', '/uploads/cv/2025/kasun_cv.pdf', 
'application/pdf', 245678, 'abc123def456', DATEADD(DAY, -30, GETDATE()), NULL);

INSERT INTO FileUploads (FileId, UserId, EntityType, EntityId, OriginalName, FilePath, FileType, FileSize, HashValue, UploadedAt, ExpiresAt)
VALUES ('FILE002', 'USR_STU_002', 'Profile', 'PROF_STU_002', 'nimali_portfolio.pdf', '/uploads/portfolio/2025/nimali_portfolio.pdf',
'application/pdf', 1234567, 'def456ghi789', DATEADD(DAY, -25, GETDATE()), NULL);

INSERT INTO FileUploads (FileId, UserId, EntityType, EntityId, OriginalName, FilePath, FileType, FileSize, HashValue, UploadedAt, ExpiresAt)
VALUES ('FILE003', 'USR_EMP_001', 'Job', 'JOB001', 'project_specs.pdf', '/uploads/jobs/2025/project_specs.pdf',
'application/pdf', 567890, 'ghi789jkl012', DATEADD(DAY, -5, GETDATE()), NULL);

PRINT 'File Uploads inserted: 3';
GO

-- =============================================
-- 19. Insert Reports
-- =============================================
PRINT 'Inserting Reports...';

INSERT INTO Reports (ReportId, GeneratedBy, ReportType, Parameters, Data, FileUrl, GeneratedDate, ExpiresAt)
VALUES ('REP001', 'ADM001', 0, 
'{"month":"October","year":"2025"}',
'{"totalUsers":19,"totalJobs":6,"totalRevenue":57000}',
'/reports/platform_report_oct2025.pdf',
DATEADD(DAY, -1, GETDATE()), DATEADD(DAY, 89, GETDATE()));

INSERT INTO Reports (ReportId, GeneratedBy, ReportType, Parameters, Data, FileUrl, GeneratedDate, ExpiresAt)
VALUES ('REP002', 'ADM001', 1,
'{"userId":"USR_STU_001","period":"Q3 2025"}',
'{"totalEarnings":235000,"completedJobs":47}',
'/reports/student_STU001_q3_2025.pdf',
DATEADD(DAY, -7, GETDATE()), DATEADD(DAY, 83, GETDATE()));

PRINT 'Reports inserted: 2';
GO

-- =============================================
-- 20. Insert OTP Verifications (Sample expired OTPs)
-- =============================================
PRINT 'Inserting OTP Verifications...';

INSERT INTO OTPVerifications (OtpId, UserId, OtpCode, Purpose, ContactMethod, IsUsed, ExpiresAt, Attempts)
VALUES ('OTP001', 'USR_STU_001', '123456', 0, 'kasun.perera@student.uom.lk', 1,
DATEADD(DAY, -30, GETDATE()), 1);

INSERT INTO OTPVerifications (OtpId, UserId, OtpCode, Purpose, ContactMethod, IsUsed, ExpiresAt, Attempts)
VALUES ('OTP002', 'USR_EMP_001', '789012', 1, '+94112223344', 1,
DATEADD(DAY, -30, GETDATE()), 1);

PRINT 'OTP Verifications inserted: 2';
GO

-- =============================================
-- Summary Report
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'MOCK DATA INSERT COMPLETE';
PRINT '========================================';
PRINT 'Job Categories: 12';
PRINT 'Users (Total): 11';
PRINT '  - Admins: 3';
PRINT '  - Students: 5';
PRINT '  - Employers: 3';
PRINT 'Profiles: 11';
PRINT 'Student Skills: 15';
PRINT 'Jobs: 6';
PRINT 'Job Applications: 6';
PRINT 'Conversations: 3';
PRINT 'Messages: 5';
PRINT 'Payments: 2';
PRINT 'Ratings: 2';
PRINT 'Earnings: 5';
PRINT 'Notifications: 4';
PRINT 'Withdrawal Requests: 3';
PRINT 'Audit Logs: 3';
PRINT 'User Sessions: 3';
PRINT 'System Configs: 5';
PRINT 'File Uploads: 3';
PRINT 'Reports: 2';
PRINT 'OTP Verifications: 2';
PRINT '========================================';
PRINT '';
PRINT 'DEFAULT PASSWORDS:';
PRINT '  Admin: Admin@123';
PRINT '  Student: Student@123';
PRINT '  Employer: Employer@123';
PRINT '';
PRINT 'All accounts are verified and active.';
PRINT 'Database ready for testing!';
PRINT '========================================';
GO

SET NOCOUNT OFF;
GO
