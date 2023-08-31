CREATE DATABASE CMS
GO
USE CMS
GO
-- User table
CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) NULL,
    Username NVARCHAR(100) NULL,
    [Password] NVARCHAR(100) NULL,
    [Role] NVARCHAR(50) NULL
);
GO
INSERT INTO [User] VALUES(N'admin@fe.edu.vn', N'Admin', N'123', N'Lecturer')
INSERT INTO [User] VALUES(N'huynqhe150250@fpt.edu.vn', N'Nguyen Quang Huy', N'123', N'Student')
GO
-- Course table
CREATE TABLE Course (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NULL,
    [Description] NVARCHAR(MAX) NULL,
    LecturerId INT NOT NULL,
    FOREIGN KEY (LecturerId) REFERENCES [User](UserId)
);
GO
-- CourseEnrollment table
CREATE TABLE CourseEnrollment (
    EnrollmentId INT PRIMARY KEY IDENTITY(1,1),
    CourseId INT NOT NULL,
    StudentId INT NOT NULL,
    FOREIGN KEY (CourseId) REFERENCES Course(CourseId),
    FOREIGN KEY (StudentId) REFERENCES [User](UserId)
);
GO
-- Quiz table
CREATE TABLE Quiz (
    QuizId INT PRIMARY KEY IDENTITY(1,1),
    CourseId INT NOT NULL,
    Title NVARCHAR(255) NULL,
    FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
);
GO
-- Question table
CREATE TABLE Question (
    QuestionId INT PRIMARY KEY IDENTITY(1,1),
    QuizId INT NOT NULL,
    Question NVARCHAR(MAX) NULL,
    OptionA NVARCHAR(MAX) NULL,
    OptionB NVARCHAR(MAX) NULL,
    OptionC NVARCHAR(MAX) NULL,
    OptionD NVARCHAR(MAX) NULL,
    CorrectOption NVARCHAR(1) NULL,
    FOREIGN KEY (QuizId) REFERENCES Quiz(QuizId)
);
GO
-- QuizAttendance table
CREATE TABLE QuizAttendance (
    AttendanceId INT PRIMARY KEY IDENTITY(1,1),
    QuizId INT NOT NULL,
    StudentId INT NOT NULL,
    Score INT NULL,
    FOREIGN KEY (QuizId) REFERENCES Quiz(QuizId),
    FOREIGN KEY (StudentId) REFERENCES [User](UserId),
    CONSTRAINT UC_QuizAttendance UNIQUE (QuizId, StudentId)
);
GO
-- Assignment table
CREATE TABLE Assignment (
    AssignmentId INT PRIMARY KEY IDENTITY(1,1),
    CourseId INT NOT NULL,
    Title NVARCHAR(255) NULL,
    [Description] NVARCHAR(MAX) NULL,
	Display BIT NOT NULL,
    Deadline DATETIME NULL,
    [File] NVARCHAR(255) NULL,
    FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
);
GO
-- StudentAssignment table
CREATE TABLE StudentAssignment (
    SubmissionId INT PRIMARY KEY IDENTITY(1,1),
    AssignmentId INT NOT NULL,
    StudentId INT NOT NULL,
    [File] NVARCHAR(255) NULL,
    SubmissionDate DATETIME NULL,
    FOREIGN KEY (AssignmentId) REFERENCES Assignment(AssignmentId),
    FOREIGN KEY (StudentId) REFERENCES [User](UserId),
    CONSTRAINT UC_StudentAssignment UNIQUE (AssignmentId, StudentId)
);

GO