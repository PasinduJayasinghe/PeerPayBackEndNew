-- =============================================
-- PeerPay Mock Data Insert Script
-- Description: Insert sample data for testing
-- Date: October 26, 2025
-- =============================================

USE [PeerPayDB]; -- Change this to your actual database name
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

PRINT 'Job Categories inserted successfully.';
GO

-- =============================================
-- 2. Insert Admin Users
-- =============================================
PRINT 'Inserting Admin Users...';

-- Admin User 1 - Super Admin
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_ADMIN_001', 'admin@peerpay.lk', '+94112650301', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 2, 'System Administrator', GETDATE(), GETDATE(), 0, 1);
    -- Password: Admin@123

INSERT INTO Admins (AdminId, UserId, Role, Permissions)
VALUES 
    ('ADM001', 'USR_ADMIN_001', 'SuperAdmin', 'ALL');

-- Admin User 2 - Content Moderator
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_ADMIN_002', 'moderator@peerpay.lk', '+94112650302', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 2, 'Content Moderator', GETDATE(), GETDATE(), 0, 1);
    -- Password: Admin@123

INSERT INTO Admins (AdminId, UserId, Role, Permissions)
VALUES 
    ('ADM002', 'USR_ADMIN_002', 'Moderator', 'VIEW_USERS,MODERATE_CONTENT,VIEW_REPORTS');

-- Admin User 3 - Support Agent
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_ADMIN_003', 'support@peerpay.lk', '+94112650303', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 2, 'Support Agent', GETDATE(), GETDATE(), 0, 1);
    -- Password: Admin@123

INSERT INTO Admins (AdminId, UserId, Role, Permissions)
VALUES 
    ('ADM003', 'USR_ADMIN_003', 'Support', 'VIEW_USERS,VIEW_TICKETS,RESPOND_TICKETS');

PRINT 'Admin Users inserted successfully.';
GO

-- =============================================
-- 3. Insert Student Users
-- =============================================
PRINT 'Inserting Student Users...';

-- Student 1 - Kasun Perera (Web Developer)
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_STU_001', 'kasun.perera@student.uom.lk', '+94771234567', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Kasun Perera', GETDATE(), GETDATE(), 0, 1);
    -- Password: Student@123

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES 
    ('STU001', 'USR_STU_001', 'University of Moratuwa', 'BSc (Hons) in Information Technology', 2, 'Verified', 4.90, 47, 235000.00, '/files/cv/kasun_perera_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_STU_001', 'USR_STU_001', 'Passionate full-stack developer with expertise in React, Node.js, and TypeScript. Love building scalable web applications.', 'Moratuwa, Western Province', '/images/profiles/kasun.jpg', '["student_id.pdf","transcript.pdf"]');

-- Student 2 - Nimali Silva (Graphic Designer)
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_STU_002', 'nimali.silva@ucsc.cmb.ac.lk', '+94772345678', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Nimali Silva', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES 
    ('STU002', 'USR_STU_002', 'University of Colombo School of Computing', 'BSc in Computer Science', 3, 'Verified', 4.80, 38, 192000.00, '/files/cv/nimali_silva_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_STU_002', 'USR_STU_002', 'Creative UI/UX designer specializing in Figma and Adobe XD. Creating beautiful, user-friendly interfaces.', 'Colombo 07', '/images/profiles/nimali.jpg', '["student_id.pdf","portfolio.pdf"]');

-- Student 3 - Ravindu Fernando (Data Analyst)
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_STU_003', 'ravindu.fernando@sliit.lk', '+94773456789', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Ravindu Fernando', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES 
    ('STU003', 'USR_STU_003', 'Sri Lanka Institute of Information Technology', 'BSc (Hons) in Information Technology specializing in Data Science', 2, 'Verified', 4.90, 52, 286000.00, '/files/cv/ravindu_fernando_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_STU_003', 'USR_STU_003', 'Data enthusiast skilled in Python, machine learning, and data visualization. Turning data into insights.', 'Malabe', '/images/profiles/ravindu.jpg', '["student_id.pdf","certifications.pdf"]');

-- Student 4 - Thisara Jayasinghe (Content Writer)
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_STU_004', 'thisara.j@pdn.ac.lk', '+94774567890', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Thisara Jayasinghe', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES 
    ('STU004', 'USR_STU_004', 'University of Peradeniya', 'BA (Hons) in English', 3, 'Verified', 4.70, 65, 162500.00, '/files/cv/thisara_jayasinghe_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_STU_004', 'USR_STU_004', 'Professional content writer with SEO expertise. Crafting engaging, optimized content for diverse audiences.', 'Kandy', '/images/profiles/thisara.jpg', '["student_id.pdf"]');

-- Student 5 - Dinuka Wickramasinghe (Video Editor)
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_STU_005', 'dinuka.w@uom.lk', '+94775678901', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Dinuka Wickramasinghe', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES 
    ('STU005', 'USR_STU_005', 'University of Moratuwa', 'BSc in Multimedia and Web Technology', 2, 'Verified', 4.85, 31, 186000.00, '/files/cv/dinuka_w_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_STU_005', 'USR_STU_005', 'Creative video editor proficient in Premiere Pro and After Effects. Bringing stories to life through video.', 'Katubedda', '/images/profiles/dinuka.jpg', '["student_id.pdf","portfolio_reel.mp4"]');

-- Student 6 - Amaya Gunasekara (Mobile Developer)
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_STU_006', 'amaya.gun@sliit.lk', '+94776789012', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Amaya Gunasekara', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES 
    ('STU006', 'USR_STU_006', 'Sri Lanka Institute of Information Technology', 'BSc in Software Engineering', 3, 'Verified', 4.75, 28, 168000.00, '/files/cv/amaya_gun_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_STU_006', 'USR_STU_006', 'Mobile app developer specializing in Flutter and React Native. Building cross-platform mobile solutions.', 'Malabe', '/images/profiles/amaya.jpg', '["student_id.pdf","certifications.pdf"]');

-- Student 7 - Sachini Perera (Digital Marketer)
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_STU_007', 'sachini.p@ucsc.cmb.ac.lk', '+94777890123', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Sachini Perera', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES 
    ('STU007', 'USR_STU_007', 'University of Colombo', 'BSc in Management and IT', 2, 'Verified', 4.60, 42, 147000.00, '/files/cv/sachini_p_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_STU_007', 'USR_STU_007', 'Digital marketing specialist focusing on social media and content strategy. Growing brands online.', 'Colombo 03', '/images/profiles/sachini.jpg', '["student_id.pdf","marketing_certs.pdf"]');

-- Student 8 - Lakshan Silva (Backend Developer)
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_STU_008', 'lakshan.s@uom.lk', '+94778901234', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 0, 'Lakshan Silva', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Students (StudentId, UserId, University, Course, YearOfStudy, AcademicVerificationStatus, Rating, CompletedJobs, TotalEarnings, CvUrl)
VALUES 
    ('STU008', 'USR_STU_008', 'University of Moratuwa', 'BSc (Hons) in Computer Science and Engineering', 4, 'Verified', 4.95, 56, 336000.00, '/files/cv/lakshan_s_cv.pdf');

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_STU_008', 'USR_STU_008', 'Backend developer expert in .NET, Java, and microservices architecture. Building robust server-side solutions.', 'Moratuwa', '/images/profiles/lakshan.jpg', '["student_id.pdf","transcript.pdf"]');

PRINT 'Student Users inserted successfully.';
GO

-- =============================================
-- 4. Insert Employer Users
-- =============================================
PRINT 'Inserting Employer Users...';

-- Employer 1 - Tech Startup
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_EMP_001', 'hr@techstartup.lk', '+94112223344', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'Tech Startup Lanka', GETDATE(), GETDATE(), 0, 1);
    -- Password: Employer@123

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES 
    ('EMP001', 'USR_EMP_001', 'Tech Startup Lanka', 'Software Company', 'Innovative tech startup building next-generation mobile and web applications for the Sri Lankan market.', 'Nimal Fernando', 'Verified', 4.80, 15);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_EMP_001', 'USR_EMP_001', 'Fast-growing tech startup looking for talented developers and designers.', 'Colombo 07', '/images/companies/techstartup.jpg', '["company_reg.pdf","tax_cert.pdf"]');

-- Employer 2 - Digital Marketing Agency
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_EMP_002', 'contact@digitalhub.lk', '+94112334455', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'Digital Hub Agency', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES 
    ('EMP002', 'USR_EMP_002', 'Digital Hub Agency', 'Marketing Agency', 'Full-service digital marketing agency specializing in social media, SEO, and content marketing.', 'Sanduni Perera', 'Verified', 4.70, 22);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_EMP_002', 'USR_EMP_002', 'Award-winning digital marketing agency serving local and international clients.', 'Colombo 03', '/images/companies/digitalhub.jpg', '["company_reg.pdf","certifications.pdf"]');

-- Employer 3 - E-commerce Platform
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_EMP_003', 'hiring@shopceylon.lk', '+94112445566', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'ShopCeylon', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES 
    ('EMP003', 'USR_EMP_003', 'ShopCeylon', 'E-commerce', 'Leading Sri Lankan e-commerce platform connecting local sellers with customers nationwide.', 'Chaminda Rathnayake', 'Verified', 4.85, 18);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_EMP_003', 'USR_EMP_003', 'Sri Lankas fastest-growing e-commerce marketplace seeking talented individuals.', 'Colombo 05', '/images/companies/shopceylon.jpg', '["company_reg.pdf","business_license.pdf"]');

-- Employer 4 - Education Technology
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_EMP_004', 'careers@edutech.lk', '+94112556677', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'EduTech Solutions', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES 
    ('EMP004', 'USR_EMP_004', 'EduTech Solutions', 'Education Technology', 'EdTech company developing online learning platforms and educational content for schools.', 'Priyanka Jayawardena', 'Verified', 4.90, 12);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_EMP_004', 'USR_EMP_004', 'Transforming education through technology. Join our mission to make learning accessible.', 'Nugegoda', '/images/companies/edutech.jpg', '["company_reg.pdf","education_license.pdf"]');

-- Employer 5 - Creative Design Studio
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_EMP_005', 'studio@creativelk.com', '+94112667788', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'Creative LK Studio', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES 
    ('EMP005', 'USR_EMP_005', 'Creative LK Studio', 'Design Studio', 'Boutique design studio specializing in branding, UI/UX design, and creative content production.', 'Dilshan Samaraweera', 'Verified', 4.75, 20);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_EMP_005', 'USR_EMP_005', 'Award-winning creative studio bringing brands to life through design.', 'Colombo 04', '/images/companies/creativelk.jpg', '["company_reg.pdf"]');

-- Employer 6 - Fintech Company
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_EMP_006', 'jobs@payfast.lk', '+94112778899', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'PayFast Lanka', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES 
    ('EMP006', 'USR_EMP_006', 'PayFast Lanka', 'Financial Technology', 'Fintech startup revolutionizing digital payments and financial services in Sri Lanka.', 'Ashan Wijesinghe', 'Verified', 4.65, 10);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_EMP_006', 'USR_EMP_006', 'Building the future of digital payments. Join our innovative team.', 'Rajagiriya', '/images/companies/payfast.jpg', '["company_reg.pdf","financial_license.pdf"]');

-- Employer 7 - Content Production House
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_EMP_007', 'info@mediapro.lk', '+94112889900', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'MediaPro Productions', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES 
    ('EMP007', 'USR_EMP_007', 'MediaPro Productions', 'Media Production', 'Full-service media production company creating video content for brands and businesses.', 'Rukshan Fernando', 'Verified', 4.80, 16);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_EMP_007', 'USR_EMP_007', 'Professional video production and content creation services.', 'Colombo 10', '/images/companies/mediapro.jpg', '["company_reg.pdf"]');

-- Employer 8 - Local Business
INSERT INTO Users (UserId, Email, Phone, PasswordHash, UserType, Name, CreatedAt, LastLogin, Status, IsVerified)
VALUES 
    ('USR_EMP_008', 'contact@greencafe.lk', '+94112990011', '$2a$11$xQKW0H5eGJfOmjvXvZQ3d.Y8KL8YT3vBvXp7FzXwP/j5nH1KqLmZW', 1, 'Green Leaf Cafe', GETDATE(), GETDATE(), 0, 1);

INSERT INTO Employers (EmployerId, UserId, CompanyName, CompanyType, Description, ContactPerson, VerificationStatus, Rating, JobsPosted)
VALUES 
    ('EMP008', 'USR_EMP_008', 'Green Leaf Cafe', 'Food & Beverage', 'Organic cafe chain expanding digital presence and seeking creative talent.', 'Malini Rodrigo', 'Verified', 4.55, 8);

INSERT INTO Profiles (ProfileId, UserId, Bio, Address, ProfilePictureUrl, Documents)
VALUES 
    ('PROF_EMP_008', 'USR_EMP_008', 'Growing cafe brand looking for social media and design support.', 'Nugegoda', '/images/companies/greenleaf.jpg', '["company_reg.pdf","health_cert.pdf"]');

PRINT 'Employer Users inserted successfully.';
GO

-- =============================================
-- Summary
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'Mock Data Insert Summary';
PRINT '========================================';
PRINT 'Job Categories: 12 inserted';
PRINT 'Admin Users: 3 inserted';
PRINT 'Student Users: 8 inserted';
PRINT 'Employer Users: 8 inserted';
PRINT '========================================';
PRINT 'Total Users: 19';
PRINT '========================================';
PRINT '';
PRINT 'Default Passwords:';
PRINT '  - Admin: Admin@123';
PRINT '  - Student: Student@123';
PRINT '  - Employer: Employer@123';
PRINT '';
PRINT 'All accounts are verified and active.';
PRINT '========================================';
GO
